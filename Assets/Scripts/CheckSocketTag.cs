using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckSocketTag : MonoBehaviour
{
    public Button checkButton;
    public GameObject buttonPilar;
    public Transform right;
    public Transform wrong;

    public Dictionary<string, Vector3[]> objectPositions = new Dictionary<string, Vector3[]>();

    void Start()
    {
        checkButton.onClick.AddListener(OnCheckButtonClick);

        right.gameObject.SetActive(false);
        wrong.gameObject.SetActive(false);
        buttonPilar.SetActive(false);

        AddObjectPositions("Procesor_S", new Vector3[] {
            new Vector3(-25.3223743f, 3.28521252f, -8.39299965f),
            new Vector3(-27.7043743f, 3.28521252f, -8.39299965f)
        });

        AddObjectPositions("ControlUnit_S", new Vector3[] {
            new Vector3(-25.3184738f, 3.40937495f, -8.39299965f),
            new Vector3(-27.7004738f, 3.40937495f, -8.39299965f)
        });

        AddObjectPositions("ALU_S", new Vector3[] {
            new Vector3(-25.369175f,3.59321237f,-8.39299965f),
            new Vector3(-27.7511749f,3.59321237f,-8.39299965f)
        });

        AddObjectPositions("In_S", new Vector3[] {
            new Vector3(-25.8829994f,3.727f,-8.39299965f),
            new Vector3(-28.2649994f,3.727f,-8.39299965f)
        });

        AddObjectPositions("Out_S", new Vector3[] {
            new Vector3(-24.7519989f,3.727f,-8.39299965f),
            new Vector3(-27.1300983f,3.727f,-8.39299965f)
        });

        AddObjectPositions("DataMemory_S", new Vector3[] {
            new Vector3(-27.5425243f,3.81651235f,-8.39299965f)
        });

        AddObjectPositions("ProgramMemory_S", new Vector3[] {
            new Vector3(-27.8603745f,3.81651235f,-8.39299965f)
        });

        AddObjectPositions("OperatingMemory_S", new Vector3[] {
            new Vector3(-25.3184738f,3.81651235f,-8.39299965f)
        });
    }

    void AddObjectPositions(string tag, Vector3[] positions)
    {
        objectPositions[tag] = positions;
    }

    public void OnCheckButtonClick()
    {
        if (CheckPositions())
        {
            right.gameObject.SetActive(true);
            wrong.gameObject.SetActive(false);
            buttonPilar.SetActive(true);
        }
        else
        {
            right.gameObject.SetActive(false);
            wrong.gameObject.SetActive(true);
            buttonPilar.SetActive(false);
        }
    }

    public bool CheckPositions()
    {
        bool allCorrect = true;

        foreach (var kvp in objectPositions)
        {
            string tag = kvp.Key;
            Vector3[] positions = kvp.Value;
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);

            foreach (var obj in objectsWithTag)
            {
                Vector3 objPosition = obj.transform.position;
                bool isCorrect = false;

                foreach (Vector3 correctPosition in positions)
                {
                    if (Vector3.Distance(objPosition, correctPosition) < 0.01f)
                    {
                        isCorrect = true;
                        break;
                    }
                }

                if (!isCorrect)
                {
                    allCorrect = false;
                }
            }
        }

        return allCorrect;
    }

}
