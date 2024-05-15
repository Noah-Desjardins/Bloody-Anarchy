using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossPhase3AttacksManager : MonoBehaviour
{
    enum HandAttack { rushPlayer, rushPlayerXtrem, shootFromTop , shootLine, bulletParry, shootArroundPlayer }
    // Start is called before the first frame update
    List<BossPhase3Hand> hands = new();
    public bool fightHasStarted = false;
    public bool fightHasEnded = false;
    void Start()
    {
        hands = GetComponentsInChildren<BossPhase3Hand>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        if (!fightHasStarted)
        {
            StartCoroutine(startHandsAttack());
            fightHasStarted = true;
        }
        innactiveHandsBack();
    }

    IEnumerator startHandsAttack()
    {
        while (!fightHasEnded)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(3f, 4f));
            int? hand = pickHand();
            if (hand != null)
            {
                switch (pickHandAttack())
                {
                    case HandAttack.rushPlayer:
                        print("rushPlayer");
                        StartCoroutine(hands[(int)hand].rushPlayer(1.5f,8f));
                    break;

                    case HandAttack.rushPlayerXtrem:
                        print("rushPlayerXtrem");
                        StartCoroutine(hands[(int)hand].rushPlayerXtrem(1f, 10f));
                        break;

                    case HandAttack.shootFromTop:
                        print("shootFromTop");
                        StartCoroutine(hands[(int)hand].shootFromTop());
                        break;

                    case HandAttack.shootLine:
                        print("shootLine");
                        StartCoroutine(hands[(int)hand].shootLine(12));
                        break;

                    case HandAttack.bulletParry:
                        print("bulletParry");
                        StartCoroutine(hands[(int)hand].bulletParry());
                        break;

                    case HandAttack.shootArroundPlayer:
                        print("shootArroundPlayer");
                        StartCoroutine(hands[(int)hand].shootArroundPlayer());
                        break;
                }
            }
        }
    }
    HandAttack pickHandAttack() => (HandAttack)Enum.GetValues(typeof(HandAttack)).GetValue(UnityEngine.Random.Range(0,6));

    void innactiveHandsBack()
    {
        foreach (BossPhase3Hand hand in hands)
        {
            if (!hand.isAttacking)
            {
                StartCoroutine(hand.backToPosition());
            }
        }
    }
    int? pickHand()
    {
        List<int> handsReady = new List<int>();
        for (int i = 0; i < hands.Count; i++)
        {
            if (!hands[i].isAttacking)
                handsReady.Add(i);
        }
        if (handsReady.Count > 0)
        {
            return handsReady[UnityEngine.Random.Range(0, handsReady.Count)] ;
        }
        else
        {
            return null;
        }
    }
    

}
