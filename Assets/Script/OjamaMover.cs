using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OjamaMover : ObjectMover
{
    public bool isCombine = false;

    protected override void Start(){
        base.Start();
    }

    protected override void Update(){

        if(isPunched){
            //rb.velocity += (new Vector2(moveSpeed, 0));
            //rb.velocity += (new Vector2(moveSpeed, 0)) / rb.mass;
            //rb.velocity += new Vector2(moveSpeed, 0) * Time.fixedDeltaTime;
            this.transform.Translate(moveSpeed, 0, 0);
        }

        if(isCombine){
            base.CallDestroyObject();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.tag == "Punch"){
            this.isPunched = true;
            audioSource.PlayOneShot(sounds[Random.Range(0, sounds.Length)]);
        }
    }

    protected override void OnCollisionEnter2D(Collision2D col){

        if(col.gameObject.tag == "Ground"){
            audioSource.PlayOneShot(blockSound);
        }
        else if(col.gameObject.tag == "Wall"){
            this.isPunched = false;
        }else {
            this.isPunched = false;
        }

        if(col.gameObject.tag == this.gameObject.tag){
            isCombine = true;
            audioSource.PlayOneShot(destroySound);
        }

        if(blockSoundCounter == 0){
            audioSource.PlayOneShot(blockSound);
            blockSoundCounter++;
        }

        foreach(Transform child in this.transform){
            GameObject.Destroy(child.gameObject);
        }
    }

}
