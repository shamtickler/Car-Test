using UnityEngine;
using System.Collections;

public class TextureScroll : MonoBehaviour {

    public float scrollSpeed = 0.5f;
    private Renderer rend;
    
	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();	
	}
	
	// Update is called once per frame
	void Update () {
        float offset = Time.time * scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));	
	}
}
