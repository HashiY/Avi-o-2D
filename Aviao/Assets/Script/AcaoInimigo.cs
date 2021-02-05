using UnityEngine;
using System.Collections;

public class AcaoInimigo : MonoBehaviour {

	GameObject objetoFelpudo;
	bool marcouPonto;

	void Start () {
		GetComponent<Rigidbody2D>().velocity = new Vector2(-4,0);  // vai para a esquerda
		objetoFelpudo = GameObject.FindGameObjectWithTag("Player");
	}

	void Update()
	{ 
		if (transform.position.x < -25) // se sair 
		{ 
			Destroy(gameObject);
		}else{
			if (transform.position.x < objetoFelpudo.transform.position.x)//se aposiçao for menor que a do jogador
			{ 
				if (!marcouPonto){
					marcouPonto=true; 
					GetComponent<Rigidbody2D>().velocity = new Vector2(-7.5f,5);//recebe um velociade

					GetComponent<Rigidbody2D>().isKinematic=false;  // faz isso para a fisica de rotaçao funcionar
					GetComponent<Rigidbody2D>().AddTorque(-50f);  // rotaçao
					GetComponent<SpriteRenderer>().color = new Color(1f,0.3f,0.3f,0.75f); // cor
					objetoFelpudo.SendMessage("Pontua");//executar a funçao de outro script
				} 
			}
		}
	}
}
