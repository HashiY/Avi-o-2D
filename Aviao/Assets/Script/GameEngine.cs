using UnityEngine;
using System.Collections;

public class GameEngine : MonoBehaviour {

	public GameObject inimigo; 

	void Comeca()
	{  
		InvokeRepeating("CriaInimigo",1,1.5f);//nome,delay,taxa de repetiçao
	}
	
	void CriaInimigo(){ 
		float alturaAleatoria = 10f * Random.value - 5;//0,0 ate 1,0
		GameObject novoInimigo = Instantiate(inimigo);//aparece
		novoInimigo.transform.position = new Vector2(15f, alturaAleatoria); 
	}

	void FinalizarJogo(){

		CancelInvoke("CriaInimigo"); //cancela
		  
	} 
}
