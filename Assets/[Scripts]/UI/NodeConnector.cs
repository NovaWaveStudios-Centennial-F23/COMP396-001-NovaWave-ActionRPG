/*
 * Created by: Han Bi
 * Description: Used to create connections between the nodes who need it
 * Last updated: October 10, 2023
 * Note: This script was mainly developed to be used with straight line connections
 * It can create diagonal lines too, but it won't look as good
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeConnector : MonoBehaviour
{
    [SerializeField]
    GameObject connectorPrefab;

    [SerializeField]
    GameObject connectorParent;

    private SkillTreeNode[] skillNodes;

    // Start is called before the first frame update
    void Start()
    {
        skillNodes = GetComponentsInChildren<SkillTreeNode>();

        for (int i = 0; i < skillNodes.Length; i++)
        {
            List<SkillTreeNode> connections = skillNodes[i].prerequisites;
            if (connections.Count > 0)
            {
                for(int j = 0; j < connections.Count; j++)
                {
                    Vector3 pos1 = skillNodes[i].gameObject.GetComponent<RectTransform>().position;
                    Vector3 pos2 = connections[j].gameObject.GetComponent<RectTransform>().position;
                    Connector connection = CreateConnector(pos1, pos2);
                    connection.SetSkillNode(skillNodes[i]);
                }
            }
        }
    }

    private Connector CreateConnector(Vector3 pos1, Vector3 pos2)
    {
        //calculate the sides of the triangle
        float b = pos1.y - pos2.y;
        float a = pos1.x - pos2.x;
        float c = Mathf.Sqrt((b * b) + (a * a));

        //calculate the angle
        float angleRad = Mathf.Atan2(b, a);
        float angleDeg = (180 * angleRad)/Mathf.PI;

        //calculate new pos
        Vector3 connectorPos = new Vector3((pos1.x + pos2.x) / 2, (pos1.y + pos2.y) / 2, 0);

        GameObject connector = Instantiate(connectorPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        connector.transform.SetParent(connectorParent.transform);

        //sets the height (length of the line) for the connector
        connector.GetComponent<RectTransform>().localScale = new Vector3(1, c, 1);
        connector.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, angleDeg+90);
        connector.GetComponent<RectTransform>().position = connectorPos;

        

        return connector.GetComponent<Connector>();
    }
}
