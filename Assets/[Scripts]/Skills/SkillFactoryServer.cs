using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SkillFactoryServer : NetworkBehaviour
{
    public static SkillFactoryServer Instance;


    private void Awake()
    {
        Instance = this;
    }

    [Command(requiresAuthority = false)]
    public void CmdCastSpell(string skill, Vector3 location, Vector3 direction, List<Stats> stats, float damage, uint playerID)
    {

        ActiveSkillSO activeSkillSO = Resources.Load<ActiveSkillSO>("Skills/" + skill + "/" + skill + "Stats");

        GameObject activeSkill = Instantiate(activeSkillSO.prefab, location, Quaternion.identity);
        activeSkillSO.allStats = stats;
        activeSkill.GetComponent<Skill>().skillSO = activeSkillSO;
        activeSkill.GetComponent<Skill>().damage = damage;
        
        if (skill == nameof(Fireball))
        {
            activeSkill.GetComponent<Fireball>().direction = direction;
        }else if(skill == nameof(FrostNova))
        {
            GameObject player = NetworkServer.spawned.TryGetValue(playerID, out NetworkIdentity identity) ? identity.gameObject : null;
            if(player != null)
            {
                activeSkill.GetComponent<FrostNova>().player = player;
            }
            else
            {
                throw new Exception($"Cannot find player by networkID: {playerID}");
            }
        }
        
        //sync any details
        NetworkServer.Spawn(activeSkill);
    }







}
