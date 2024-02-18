using UnityEngine;
using Random = UnityEngine.Random;

public class RecoilLogic : MonoBehaviour
{
    private Vector3 currentRotation,targetRotation;
    [SerializeField] float recoilX,recoilY,recoilZ, snap, returnSpeed;

    private void Update() {
        switch (WeaponManager.weaponIndex)
        {   
            case 1:
                recoilX = -2;
                recoilY = 2;
                break;

            case 2:
                recoilX = -3.5f;
                recoilY = 4;
                break;

            case 3:
                recoilX = -2f;
                recoilY = 2f;
                break;

            case 4:
                recoilX = -20;
                recoilY = 10;
                break;

            default:
                break;
        }
        Debug.Log(recoilX);
        Debug.Log(recoilY);

        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snap * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }


    public void Recoil(){
        targetRotation += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ,recoilZ));        
    }
}
