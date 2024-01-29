using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーのステータス管理クラス
public class PlayerStatusManager : MonoBehaviour
{
    //ステータスの初期化用
    [SerializeField] private float maxHP;       //最大体力      
    [SerializeField] private float attackDamage;//近距離攻撃ダメージ
    [SerializeField] private float shotDamage;  //遠距離攻撃ダメージ
    [SerializeField] private int criChance;     //クリティカル率
    [SerializeField] private float criDamage;   //クリティカルダメージ
    [SerializeField] private int initialMoney;  //初期所持金
    [SerializeField] private float[] barrier = new float[5];  //バリアの倍率 最初は要素0番
    
    ItemDataS itemEffect;   //アイテムの追加効果

    public bool onSelfHarm = false; //アイテム用フラグ

    public bool onResetHpBr = false;    //体力、バリアリセットしたか
    public PlayerStatusData status;     //プレイヤーのステータス格納用


    PlayerItemManager playerItemManager;

    // Start is called before the first frame update
    void Start()
    {
        //スクリプト取得
        playerItemManager = GetComponent<PlayerItemManager>();

        //ステータスの初期化
        status = new PlayerStatusData(maxHP, initialMoney, attackDamage, shotDamage, barrier[0], criChance, criDamage);

        Debug.Log("maxHP" + status.MaxHP);
    }

    //所持アイテム効果取得関数
    public void GetEffect()
    {
        //PIMにitemEffectを参照渡しして、データを取得
        playerItemManager.GetItemEffect(ref itemEffect);
    }

    //ダメージ計算関数 引数が１つならクリティカルは起きない
    public float DamageCalc(float damage, int criC = -100, float criD = 1f)
    {
        GetEffect();//所持アイテム効果取得

        //1~100のランダム
        int dice = Random.RandomRange(1, 101);

        //ランダムの値が、クリティカル率以下なら
        if (dice <= criC + itemEffect.CriChance)
        {
            //クリティカルダメージを加える
            return damage * itemEffect.Attack * (criD + itemEffect.CriDamage);
        }
        else
        {
            //通常のダメージ
            return damage * itemEffect.Attack;
        }
    }
    //体力計算関数
    public float HPCalc(float hp, float damage, float barrier = 1.0f)
    {
        GetEffect();//所持アイテム効果取得

        return hp - (damage * itemEffect.Block * barrier);
    }
    //体力割合回復関数
    public float HealHPper(float maxhp, float hp, float per)
    {
        float heal = maxhp * per;

        //回復して最大体力を超えるなら
        if (hp + heal >= maxhp)
        {
            //現在の体力を最大体力と同じにする
            return maxhp;
        }
        else
        {
            //現在の体力を回復する
            return hp + heal;
        }
    }

    

    //ダメージを受ける関数
    public bool TakeDamage(float damage)
    {
        Debug.Log("ダメージを食らった");

        //HP計算関数を呼んで、現在体力を更新
        status.CurrentHP = HPCalc(status.CurrentHP, damage, status.Barrier);

        //HPが０以下だったらfalseを返す
        //直接シーンを変更してもいい
        if (status.CurrentHP <= 0)
            return false;
        else
            return true;
    }

    //近距離攻撃のダメージ計算関数
    public float AttackDamageCalc()
    {
        GetEffect();//所持アイテム効果取得

        //AttackDamage[0]を引数として、ダメージ計算関数を呼ぶ
        return DamageCalc(status.GetAttackDamage(0), status.CriChance, status.CriDamage)
                * itemEffect.SwordAttack;
    }

    //遠距離攻撃のダメージ計算関数
    public float ShotDamageCalc()
    {
        GetEffect();//所持アイテム効果取得

        //SelfHarmを持っているかつ体力が１より多いなら 削除予定
        if (onSelfHarm == true && status.CurrentHP >1f)
        {
            //最大体力の5%を受ける
            TakeDamage(MaxHP() * 0.05f);
        }

        //AttackDamage[1]を引数として、ダメージ計算関数を呼ぶ
        return DamageCalc(status.GetAttackDamage(1), status.CriChance, status.CriDamage)
                * itemEffect.ShotAttack;
    }

    //バリアの倍率更新関数
    public void ChangeBarrier(int n)
    {
        //受け取った引数番目の要素に更新
        status.Barrier = barrier[n];
    }
    //体力、バリアリセット関数
    public void ResetHpBr()
    {
        status.CurrentHP = status.MaxHP;
        status.Barrier = barrier[0];

        onResetHpBr = true;//バリアゲージの初期化用
    }
    //現在の体力の割合を求める関数
    public float HPper()
    {
        return status.CurrentHP / status.MaxHP;
    }
    //最大体力関数
    public float MaxHP()
    {
        GetEffect();//所持アイテム効果取得

        status.MaxHP = maxHP * itemEffect.MaxHp;

        //現在HPが最大HPを超えているなら調整
        if(status.CurrentHP > status.MaxHP)
        {
            status.CurrentHP = status.MaxHP;
        }
        return status.MaxHP;
    }
    //現在の体力を調整する関数
    public void AdjustHP()
    {
        //現在の体力が最大体力を超えていたら
        if(status.CurrentHP > status.MaxHP)
        {
            //現在の体力を最大体力に更新
            status.CurrentHP = status.MaxHP;
        }
    }

    //お金獲得関数
    public void GettingMoney(int money)
    {
        GetEffect();//所持アイテム効果取得

        Debug.Log("お金取得");
        status.Money += (int)(money * itemEffect.AddMoney);
    }
    //お金を使ったとき関数
    public void UseMoney(int money)
    {
        status.Money -= money;
    }
}

