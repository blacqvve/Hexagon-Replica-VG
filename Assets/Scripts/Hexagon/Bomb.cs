using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bomb : Hexagon
{

     [SerializeField]
     private int timer;
     [SerializeField]
     private Text text;


     private Material material;
     private float dissolveAmount;
     private float dissolveSpeed;
     private bool isDissolving;
     private void Awake()
     {
          gridManager = GridManager.Instance;
          text.text = timer.ToString();
     }
     private void OnEnable()
     {
          material = GetComponent<SpriteRenderer>().material;
          gridManager = GridManager.Instance;
          timer = HexMetrics.BOMB_TIMER;
          text.text = timer.ToString();
          gridManager.BombTick += HandleTimer;
     }

     private void Update()
     {
          if (isDissolving)
          {
               dissolveAmount = Mathf.Clamp01(dissolveAmount + dissolveSpeed * Time.deltaTime);
               material.SetFloat("_DissolveAmount", dissolveAmount);
          }
          else
          {
               dissolveAmount = Mathf.Clamp01(dissolveAmount - dissolveSpeed * Time.deltaTime);
               material.SetFloat("_DissolveAmount", dissolveAmount);
          }
     }
     private void HandleTimer()
     {

               timer--;
               if (timer == 0)
               {
               MenuManager.Instance.GameOver();
               }
               text.text = timer.ToString();  
     }

     private void OnDisable()
     {
          gridManager.BombTick -= HandleTimer;
          gridManager = null;
     }

}
