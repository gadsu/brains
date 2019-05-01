using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField]
    private Material _material;
    [SerializeField]
    private float speedScaleX, speedScaleY;

    private Renderer _renderer;

    public static float x = 0f;
    public static float y = 0f;
    public static float approachRandomY = 0f, markRandomY = 0f;

    public static float swapScale = 0f;
	// Use this for initialization
	void Start () {
        _renderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        approachRandomY += ((y - markRandomY) * Time.deltaTime) * speedScaleY;

        if (Mathf.Abs(approachRandomY) >= Mathf.Abs(y))
        {
            y = Random.Range(-0.01f, 0.01f);
            markRandomY = approachRandomY;
        }

        x += Time.deltaTime * speedScaleX;

        if (x > 1f)
            x -= 1f;

        if (swapScale > 2f)
        {
            _material.mainTextureScale.Set(_material.mainTextureScale.x, -_material.mainTextureScale.y);
        }

        swapScale += Time.deltaTime * speedScaleX * speedScaleY;

        _material.SetTextureOffset("_MainTex", new Vector2(x, approachRandomY));
    }

    private void FixedUpdate()
    {
        _material.SetTextureScale("_MainTex", new Vector2(1.0f - Mathf.Sin(Time.time) * 0.015f, 1.0f - Mathf.Sin(Time.time) * 0.015f));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.name == "Spud")
        {
            GameObject.Find("GameStateController").GetComponent<GameStateHandler>().SetState(GameStateHandler.GameState.Lost);
        }
    }
}
