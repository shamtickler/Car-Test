#pragma strict

var frCollider : WheelCollider;
var flCollider : WheelCollider;
var rrCollider : WheelCollider;
var rlCollider : WheelCollider;

var frTireCollider : Transform;
var flTireCollider : Transform;
var rrTireCollider : Transform;
var rlTireCollider : Transform;

function Start () {

}

function Update () {
var pos : Vector3;
var quat : Quaternion;
frCollider.GetWorldPose(pos,quat);
frTireCollider.position = pos;
frTireCollider.rotation = quat;
flCollider.GetWorldPose(pos,quat);
flTireCollider.position = pos;
flTireCollider.rotation = quat;
rlCollider.GetWorldPose(pos,quat);
rlTireCollider.position = pos;
rlTireCollider.rotation = quat;
rrCollider.GetWorldPose(pos,quat);
rrTireCollider.position = pos;
rrTireCollider.rotation = quat;
}