using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatorMover : ObjectMover
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        // this.transform.Translate(-0.05f, 0, 0);
        // this.transform.Rotate(10f, 0, 0);
        // if(this.transform.position.x <= -12f){
        //     base.DestroyObject();
        // }

    }

    protected override void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.tag == "Player"){
            // base.DestroyObject();

            if(this.gameObject.tag == "Operator_plus"){
                nlm.SetOperator("+");
            }
            if(this.gameObject.tag == "Operator_minus"){
                nlm.SetOperator("-");
            }
            if(this.gameObject.tag == "Operator_times"){
                nlm.SetOperator("*");
            }
            if(this.gameObject.tag == "Operator_divide"){
                nlm.SetOperator("/");
            }
            if(this.gameObject.tag == "Operator_percent"){
                nlm.SetOperator("%");
            }
            if(this.gameObject.tag == "Operator_equal"){
                nlm.SetOperator("=");
            }
        }
    }
    protected override void OnCollisionEnter2D(Collision2D col){
    }
}
