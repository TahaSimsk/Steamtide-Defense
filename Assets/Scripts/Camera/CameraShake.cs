using System.Collections;
using System.Threading;
using UnityEngine;

class CameraShake : MonoBehaviour
{
    [SerializeField] GameEvent2ParamSO onTowerPlaced;

    [Range(0f, 1f)][SerializeField] float trauma;
    [SerializeField] float traumaMultiplier = 5f; //the power of the shake
    [SerializeField] float traumaMagnitude = 0.8f; //the range of movment
    [SerializeField] float traumaRotationMagnitude = 17f; //the rotational power
    [SerializeField] float traumaDepthMagnitude = 1.3f; //the depth multiplier
    [SerializeField] float traumaDecay = 1.3f; //how quickly the shake falls off

    float timeCounter = 0f; //counter stored for smooth transition

    Quaternion originalRot;
    Vector3 originalPos;

    private void Awake()
    {
        originalRot = transform.localRotation;
        originalPos = transform.localPosition;
    }

    private void OnEnable()
    {
        //onTowerPlaced.onEventRaised += (x,y) => StartCoroutine(Shake(1));
        onTowerPlaced.onEventRaised += ShakeCam;
    }
    private void OnDisable()
    {
        //onTowerPlaced.onEventRaised -= (x,y) => StartCoroutine(Shake(1));
        onTowerPlaced.onEventRaised -= ShakeCam;
    }

    public float Trauma //accessor is used to keep trauma within 0 to 1 range
    {
        get
        {
            return trauma;
        }
        set
        {
            trauma = Mathf.Clamp01(value);
        }
    }


    float GetFloat(float seed) //Get a perlin float between -1 & 1, based off the time counter.
    {
        return (Mathf.PerlinNoise(seed, timeCounter) - 0.5f) * 2;
    }


    Vector3 GetVec3() //use the above function to generate a Vector3, different seeds are used to ensure different numbers
    {                                                 //depth modifier applied here
        return new Vector3(GetFloat(1), GetFloat(10), GetFloat(100) * traumaDepthMagnitude);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.G))
        {
            StartCoroutine(Shake(1));
        }
    }

    public void ShakeCam(object ss, object dd)
    {
        StartCoroutine(Shake(1f));
    }


    public IEnumerator Shake(float _amount)
    {
        if (Trauma > 0)
        {
            Trauma += _amount;
            yield break;
        }

        Trauma += _amount;

        originalPos = transform.localPosition;
        originalRot = transform.localRotation;

        while (Trauma > 0.05f)
        {
            //increase the time counter (how fast the position changes) based off the traumaMult and some root of the Trauma
            timeCounter += Time.unscaledDeltaTime * Mathf.Pow(trauma, 0.3f) * traumaMultiplier;

            //Bind the movement to the desired range
            Vector3 newPos = GetVec3() * traumaMagnitude * Trauma;

            //rotation modifier applied here
            transform.localPosition = newPos + originalPos;
            transform.localRotation = originalRot * Quaternion.Euler(newPos * traumaRotationMagnitude);

            //decay faster at higher values
            Trauma -= Time.unscaledDeltaTime * traumaDecay * Trauma;
            yield return null;
        }


        while (transform.localPosition != originalPos && transform.localRotation != originalRot)
        {
            Vector3 newPos = Vector3.Lerp(transform.localPosition, originalPos, Time.unscaledDeltaTime * 500);
            transform.localPosition = newPos;
            Quaternion newRot = Quaternion.Lerp(transform.localRotation, originalRot, Time.unscaledDeltaTime * 500);
            transform.localRotation = newRot;
            yield return null;
        }

        Trauma = 0;

    }
}