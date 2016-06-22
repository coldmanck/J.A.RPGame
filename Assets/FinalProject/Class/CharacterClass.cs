using UnityEngine;
using System.Collections;
namespace FinalProject.Character
{
    public class CharacterClass : MonoBehaviour
    {

        private int mLevel;
        private int mExp;
        private int mAttack;
        private int mDef;
        private int mDex;
        private int mHp;
        // Use this for initialization
        void Start()
        {
            mLevel = 1;
            mExp = 0;
            mAttack = 10;
            mDef = 10;
            mDex = 10;
            mHp = 20;
        }

        // Update is called once per frame
        void Update()
        {

        }

        void setLevel(int tmpValue)
        {
            mLevel = tmpValue;

        }
        void setExp(int tmpValue)
        {
            mExp = tmpValue;

        }
        void setAttack(int tmpValue)
        {
            mAttack = tmpValue;

        }
        void setDef(int tmpValue)
        {
            mDef = tmpValue;

        }
        void setDex(int tmpValue)
        {
            mDex = tmpValue;

        }

        void setHp(int tmpValue)
        {
            mHp = tmpValue;

        }

        int getLevel()
        {
            return mLevel;

        }
        int getExp()
        {
            return mExp;

        }
        int getAttack()
        {
            return mAttack;

        }
        int getDef()
        {
            return mDef;

        }
        int getDex()
        {
            return mDex;

        }

        int getHp()
        {
            return mHp;

        }
    }






}