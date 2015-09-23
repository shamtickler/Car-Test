#pragma strict

var particle : GameObject;
var layerMask : LayerMask;
var hit : RaycastHit;
var spawnPosition : Transform;

function Start () {

}

function Update () {

if (Input.GetButtonDown("Fire1")){

var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
 
if (Physics.Raycast (ray, hit, 1000)) {

var shootDirection : Vector3;
shootDirection.y = spawnPosition.position.y;
shootDirection.x=hit.point.x;
shootDirection.z=hit.point.z;

var spawnedEffect : GameObject = Instantiate(particle, spawnPosition.position, transform.rotation);
spawnedEffect.transform.LookAt(shootDirection, Vector3.up);

} 





}



}