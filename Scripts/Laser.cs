﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;

    public bool _isEnemyLaser = false;
    
    

    // Update is called once per frame
    void Update()
    {
        if (_isEnemyLaser==false)
        {
            MoveUP();
        }
        else
        {
            MoveDown();
        }
    }

    void MoveUP()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y >= 8f)
        {
            if (this.transform.parent != null)
            {
                Destroy(this.transform.parent.gameObject);
            }

            Destroy(this.gameObject);



        }
    }

    void MoveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -8f)
        {
            if (this.transform.parent != null)
            {
                Destroy(this.transform.parent.gameObject);
            }

            Destroy(this.gameObject);

        }
    }

    public void SetEnemyLaser()
    {
        
        this._isEnemyLaser = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && _isEnemyLaser == true)
        {
            Player player = collision.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }
        }
    }
}
