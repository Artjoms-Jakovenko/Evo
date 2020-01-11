using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiData
{
    public static Dictionary<StatName, StatUI> statDescriptions = new Dictionary<StatName, StatUI>()
    {
        { StatName.Speed, new StatUI()
        {
            statDisplayName = "Speed",
            statDescription = "How fast the blob moves. More speed will help the blob outrun other species.",
            statResourceImagePath = "UI/Stats/SpeedICon" // TODO load image object instead of path
        }
        },
        { StatName.Health, new StatUI()
        {
            statDisplayName = "Health",
            statDescription = "How much damage a blob can take before it dies. More health means the blob will likely survive longer.",
            statResourceImagePath = "UI/Stats/HealthIcon"
        }
        },
        { StatName.Sight, new StatUI()
        {
            statDisplayName = "Sight",
            statDescription = "How far a blob can notice things. More sight will help the blob make better decisions and react faster.",
            statResourceImagePath = "UI/Stats/HealthIcon" // TODO
        }
        },
        { StatName.ReactionTime, new StatUI()
        {
            statDisplayName = "Reaction time",
            statDescription = "How fast a blob can make decisions.", // TODO
            statResourceImagePath = "UI/Stats/HealthIcon" // TODO
        }
        },
        { StatName.MaxEnergy, new StatUI()
        {
            statDisplayName = "Max energy",
            statDescription = "How much energy can a blob store. Having a lot of energy helps blob not starve and lets blob perform special actions.", // TODO
            statResourceImagePath = "UI/Stats/HealthIcon" // TODO
        }
        },
    };
}
