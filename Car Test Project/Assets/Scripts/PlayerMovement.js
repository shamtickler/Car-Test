#pragma strict
private var tempVec : Vector3;
var movementSpeed : float;
var myCamera : GameObject;
function Start () {

}

function Update () {
//transform.rotation.y = myCamera.transform.rotation.y;
transform.rotation.x = 0;
transform.rotation.z = 0;



 if(Input.GetKey(KeyCode.A)){
             myCamera.transform.Translate(Vector3.left * movementSpeed * Time.deltaTime, Space.World);
  }
if(Input.GetKey(KeyCode.D)){
             myCamera.transform.Translate(Vector3.right * movementSpeed * Time.deltaTime, Space.World);
  }
  if(Input.GetKey(KeyCode.W)){
  
             tempVec = (Vector3.forward * movementSpeed * Time.deltaTime);
             tempVec.y = 0;
             myCamera.transform.Translate(tempVec, Space.Self);
  }
  if(Input.GetKey(KeyCode.S)){
            myCamera.transform.Translate(Vector3.back * movementSpeed * 0.5 * Time.deltaTime, Space.World);
  }
}