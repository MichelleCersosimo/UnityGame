using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeText : MonoBehaviour {
	public Text txt;

	//private ActionWaterPH AW;
	private string hola;

	// Update is called once per frame
	public void Change() {

		hola= SelectPH(hola); 
		txt.text = hola;
			
	}

	public string SelectPH(string resultado){
		
		float choice = Random.Range(0.0F, 14.0F);
		
		if(choice > 0.0 && choice < 2.5){
			resultado="El papel indicador presenta un color entre rojo y vino, cerca del 2.5 en la escala de PH, este valor indica que el agua presenta un estado bastante acido, a este nivel sustancias como los refrescos gaseosos, restos de acido de bateria o vinagre podrian estar mezclados con el agua.";
			
		}else{
			if(choice > 2.6 && choice < 4.9){
				resultado="El papel indicador presenta un color naranja oscuro, cerca del 4.2 en la escala de PH, este valor indica que el agua esta en un estado acido medio, a este nivel sustancias como la cervez y la lluvia acida podrian estar mezcladas con el agua. Los peces adultos del rio no pueden sobrevivir con este nivel de PH, por lo que los peces de esta zona se han extinguido";
				
			}else{
				if(choice > 5.0 && choice < 5.5){
					resultado="El papel indicador presenta un color entre naranja y amarillo, cerca del 5.0 en la escala de PH, este valor indica que el agua presenta un estado acido leve, a este nivel restos de cultivos de banano o cafe podrian estar mezclados con el agua. Los peces pueden sobrevivir a estos niveles de agua, sin embargo su reproduccion resulta afectada. De igual manera los huevos de criaturas como la rana, los rencuajos, los cangrejos de rio y las efimeras mueren.";
					
				}else{
					if(choice > 5.6 && choice < 6.0){
						resultado="El papel indicador presenta un color amarillento, cerca del 5.6 en la escala de PH, este valor indica que el agua presenta un estado neutral con restos acidos, la lluvia limpia esta a un nivel de 6.0, por lo que este nivel no es perfudicial para la mayoria de las criaturas, sin embargo algunas truchas arco iris no resisten y perecen. Sustancias como la leche podrian tambien estar mezcladas con el agua";
						
					}else{
						if(choice > 6.1 && choice < 7.0){
							resultado="El papel indicador presenta un color entre amarillo y verde claro, cerca del 7.0 en la escala de PH, este valor indica que el agua presenta un estado neutro, El agua no presenta contaminates y es adecuada para la vida de animales de rio.";
							
						}else{
							if(choice > 7.1 && choice < 10.0){
								resultado="El papel indicador presenta un color entre verde claro y azul marino, cerca del 8.0 en la escala de PH este valor indica que el agua presenta un estado base leve, a este nivel sustancias como el acido urico o restos de jabon y sustancias que contienen Hidroxido de sodio en cantidades bajas o bicarbonato de sodio podrian estar mezclados con el agua.";
								
							}else{
								if(choice > 10.1 && choice < 11.5){
									resultado="El papel indicador presenta un color Azul oscuro, cerca del 11,5 en la escala de PH, este valor indica que el agua presenta un estado base medio, a este nivel restos de pesticidas o resina que utilizan Amoniaco podrian estar mezclados con el agua.";
									
								}else{
									if(choice > 11.6 && choice < 14.0){
										resultado="El papel indicador presenta un color entre azul oscuro y morado, cerca del 12 o 13 en la escala de PH, este valor indica que el agua presenta un estado base alte, grandes concentraciones de Hidroxido de Sosio o Hipoclorito de sodio estan presentes en el agua,productos como los blanqueadores o limpiadores para desagues contienen estas sustancias.";
										
									}
								}
							}
						}
					}
				}
			}
		}
		return resultado;
	}
}