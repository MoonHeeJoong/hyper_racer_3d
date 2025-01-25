using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class TestScripts
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestScriptsSimplePasses()
    {
        // Use the Assert class to test conditions
        
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestScriptsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        SceneManager.LoadScene("HyperRacer3D");
        
        yield return WaitForSceneLoad();

        GameManager gameObj = GameObject.FindObjectOfType<GameManager>();
        Assert.IsNotNull(gameObj);

        Button startButton = GameObject.Find("StartButton").GetComponent<Button>();
        startButton.onClick.Invoke();

        yield return new WaitForSeconds(1);

        Assert.AreEqual(gameObj.gameState, GAMESTATE.PLAYING);

        yield return GameLoop();

        yield return null;
    }

    //wait for scene load
    IEnumerator WaitForSceneLoad(){
        while(SceneManager.GetActiveScene().name != "HyperRacer3D"){
            yield return null;
        }
    }

    IEnumerator GameLoop(){
        GameManager gameObj = GameObject.FindObjectOfType<GameManager>();
        MockInputProvider inputProvider = new MockInputProvider();
        InputHandler.Instance.SetInputProvider(inputProvider);

        Vector3 gasPositionPos1X = new Vector3(-1.5f, 0.1f, 0);
        Vector3 gasPositionPos2X = new Vector3(-0.5f, 0.1f, 0);
        Vector3 gasPositionPos3X = new Vector3(0.5f, 0.1f, 0);
        Vector3 gasPositionPos4X = new Vector3(1.5f, 0.1f, 0);

        List<Vector3> gasPositions = new List<Vector3>(){gasPositionPos1X, gasPositionPos2X, gasPositionPos3X, gasPositionPos4X};
        

        float elapsedTime = 0;
        float targetTime = 30;
        
        //Task.Run(() => MoveCar(inputProvider, gameObj, gasPositions));


        
        
        while(gameObj.gameState == GAMESTATE.PLAYING)
        {
            elapsedTime += Time.deltaTime;
            inputProvider.SetAxisValue("Vertical", 1);

            foreach(Vector3 defaultGasPos in gasPositions){
                RaycastHit hit;
                Vector3 gasPos = new Vector3(defaultGasPos.x, defaultGasPos.y, gameObj.car.transform.position.z);

                if(Physics.Raycast(gasPos, gasPos + Vector3.forward, out hit, 10f)){
                    if(gameObj.car.transform.position.x - hit.transform.position.x > 0.3f ){
                        inputProvider.SetAxisValue("Horizontal", -1);
                    }
                    else if(gameObj.car.transform.position.x - hit.transform.position.x < -0.3f )
                    {
                        inputProvider.SetAxisValue("Horizontal", 1);   
                    }
                    else{
                        inputProvider.SetAxisValue("Horizontal", 0);
                    }
                }
                
            }
            yield return new WaitForSeconds(0.1f);
        }

        Assert.Less(elapsedTime, targetTime, "Game should end in 30 seconds");
    }

}
