﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UiData
{
    public static Dictionary<StatName, StatUI> statDescriptions = new Dictionary<StatName, StatUI>()
    {
        { StatName.Speed, new StatUI("UI/Stats/SpeedICon")
        {
            statDisplayName = "Speed",
            statDescription = "How fast the blob moves. More speed will help the blob outrun other species.",
        }
        },
        { StatName.Health, new StatUI("UI/Stats/HealthIcon")
        {
            statDisplayName = "Health",
            statDescription = "How much damage a blob can take before it dies. More health means the blob will likely survive longer.",
        }
        },
        { StatName.Sight, new StatUI("UI/Stats/HealthIcon") // TODO
        {
            statDisplayName = "Sight",
            statDescription = "How far a blob can notice things. More sight will help the blob make better decisions and react faster.",
        }
        },
        { StatName.ReactionTime, new StatUI("UI/Stats/HealthIcon") // TODO
        {
            statDisplayName = "Reaction time",
            statDescription = "How fast a blob can make decisions.", // TODO
        }
        },
        { StatName.MaxEnergy, new StatUI("UI/Stats/HealthIcon") // TODO
        {
            statDisplayName = "Max energy",
            statDescription = "How much energy can a blob store. Having a lot of energy helps blob not starve and lets blob perform special actions.", // TODO
        }
        },
        { StatName.Strength, new StatUI("UI/Stats/HealthIcon") // TODO
        {
            statDisplayName = "Strength",
            statDescription = "How powerful this blob's hits are. Having a lot of strength helps fight other blobs.", // TODO
        }
        },
    };

    public static Dictionary<BlobType, BlobUI> blobTypeDescription = new Dictionary<BlobType, BlobUI>()
    {
        { BlobType.Survivor, new BlobUI("TODO") // "TODO"
        {
        }
        },
        { BlobType.Fighter, new BlobUI("TODO") // "TODO"
        {
        }
        },
    };

    public static Dictionary<ActionEnum, ActionIconUI> actionIconDescription = new Dictionary<ActionEnum, ActionIconUI>()
    {
        { ActionEnum.Eat, new ActionIconUI("UI/ActionIcons/me")
        {

        }
        },
        { ActionEnum.MeleeFight, new ActionIconUI("TODO")
        {

        }
        },
        { ActionEnum.RunAway, new ActionIconUI("TODO")
        {

        }
        },
    };

    public static Dictionary<InventoryEnum, RewardUI> inventoryDescription = new Dictionary<InventoryEnum, RewardUI>()
    {
        { InventoryEnum.Money, new RewardUI("TODO") // "TODO"
        {
        }
        },
        { InventoryEnum.PremiumMoney, new RewardUI("TODO") // "TODO"
        {
        }
        },
        { InventoryEnum.StartChest, new RewardUI("TODO") // "TODO"
        {
        }
        },
        { InventoryEnum.Capsule, new RewardUI("TODO") // "TODO"
        {
        }
        },
    };
}
