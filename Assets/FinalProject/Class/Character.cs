using UnityEngine;
using System.Collections;

[System.Serializable]	// 讀存檔用的
public class Character{

	// Use this for initialization
	private int mLevel;		// 等級
	private int mExp;		// 經驗值
	private int mAttack;	// 攻擊力
	private int mDef;		// 防禦力
	private int mDex;		// 敏捷度
	private int mHp;		// 血量
	private int mPt;		// 點數

	/*********************************************************/

	public Character(int LV, int EXP, int ATK, int DEF, int DEX, int HP, int PT) {
		this.mLevel = LV;
		this.mExp = EXP;
		this.mAttack = ATK;
		this.mDef = DEF;
		this.mDex = DEX;
		this.mHp = HP;
		this.mPt = PT;
	}

	public void setLevel(int tmpValue)
	{
		mLevel = tmpValue;

	}
	public void setExp(int tmpValue)
	{
		mExp = tmpValue;

	}
	public void setAttack(int tmpValue)
	{
		mAttack = tmpValue;

	}
	public void setDef(int tmpValue)
	{
		mDef = tmpValue;

	}
	public void setDex(int tmpValue)
	{
		mDex = tmpValue;

	}

	public void setHp(int tmpValue)
	{
		mHp = tmpValue;

	}

	public void setPt(int tmpValue)
	{
		mPt = tmpValue;
	}

	public int getLevel()
	{
		return mLevel;

	}
	public int getExp()
	{
		return mExp;

	}
	public int getAttack()
	{
		return mAttack;

	}
	public int getDef()
	{
		return mDef;

	}
	public int getDex()
	{
		return mDex;

	}

	public int getHp()
	{
		return mHp;

	}

	public int getPt()
	{
		return mPt;
	}
}
