using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour { 
    public Character mChar;

    // public 道具....
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    } 
    

    public void setLevel(int tmpValue)
    {
        mChar.setLevel(tmpValue);

    }
    public void setExp(int tmpValue)
    {
        mChar.setExp(tmpValue);

    }
    public void setAttack(int tmpValue)
    {
        mChar.setAttack(tmpValue);

    }
    public void setDef(int tmpValue)
    {
        mChar.setDef(tmpValue);

    }
    public void setDex(int tmpValue)
    {
        mChar.setDex(tmpValue);

    }

    public void setHp(int tmpValue)
    {
        mChar.setHp(tmpValue);

    }


    public int getLevel()
    {
        return mChar.getLevel();

    }
    public int getExp()
    {
        return mChar.getExp();

    }
    public int getAttack()
    {
        return mChar.getAttack();

    }
    public int getDef()
    {
        return mChar.getDef();

    }
    public int getDex()
    {
        return mChar.getDex();

    }

    public int getHp()
    {
        return mChar.getHp();

    }

 

}
