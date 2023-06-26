using MelonLoader;
using DeadlyDarts;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using UnityEngine.Assertions;
using Il2CppAssets.Scripts.Simulation.SMath;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Extensions;

[assembly: MelonInfo(typeof(DeadlyDarts.DeadlyDarts), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace DeadlyDarts;

public class DeadlyDarts : BloonsTD6Mod
{
    public bool ingame;
    public override void OnApplicationStart()
    {
        ModHelper.Msg<DeadlyDarts>("DeadlyDarts loaded!");
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (ingame == true)
        {
            var v3 = UnityEngine.Input.mousePosition;
            v3.z = 0;
            v3 = InGame.instance.sceneCamera.ScreenToWorldPoint(v3);

            float x = v3.x;
            float y = v3.y * (-2.3f);

            foreach (var proj in InGame.instance.bridge.GetAllProjectiles())
            {
                foreach (var tower in InGame.instance.bridge.GetAllTowers())
                {
                    if (proj.EmittedBy == tower.tower)
                    {
                        continue;
                    }
                    else if (proj.Position.ToVector2().Distance(tower.simPosition.ToVector2()) < 10f)
                    {
                        tower.tower.SellTower();

                    }
                }
            }
        }
    }
    public override void OnMatchEnd()
    {
        base.OnMatchEnd();
        ingame = false;
    }
    public override void OnMatchStart()
    {
        base.OnMatchStart();
        ingame = true;
    }
}