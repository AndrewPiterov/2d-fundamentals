using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	Rigidbody2D _rigid;
	SpriteRenderer mySpriteRenderer;
	Animator _anim;

	bool _canMove = true;

	bool facingRight = true;

	public float maxSpeed;

	// jump
	bool _grounded = false;
	float _groundCheckRedius = .2f;
	public LayerMask graundLayer;
	public Transform groundCheck;
	public float jumpPower;

	// Use this for initialization
	void Start () {
		_rigid = GetComponent<Rigidbody2D> ();
		mySpriteRenderer = GetComponent<SpriteRenderer> ();
		_anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (_canMove && _grounded && Input.GetAxis ("Jump") > 0) {
			_anim.SetBool ("IsGrounded", false);
			_rigid.velocity = new Vector2 (_rigid.velocity.x, 0f);
			_rigid.AddForce (new Vector2 (0, jumpPower), ForceMode2D.Impulse);
			_grounded = false;
		}

		_grounded = Physics2D.OverlapCircle (groundCheck.position, _groundCheckRedius, graundLayer);
		_anim.SetBool ("IsGrounded", _grounded);

		var move = Input.GetAxis ("Horizontal");

		if (_canMove) {
			if ((move > 0 && !facingRight) || (move < 0 && facingRight)) {
				Flip ();
			}
			_rigid.velocity = new Vector2 (move * maxSpeed, _rigid.velocity.y);
			_anim.SetFloat ("MoveSpeed", Mathf.Abs (move));
		} else {
			_rigid.velocity = new Vector2 (0, _rigid.velocity.y);
			_anim.SetFloat ("MoveSpeed", 0);
		}
	}

	void Flip(){
		facingRight = !facingRight;
		mySpriteRenderer.flipX = !mySpriteRenderer.flipX;
	}

	public void ToggleCanMove(){
		_canMove = !_canMove;
	}
}
