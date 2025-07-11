using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageConfigs", menuName ="Stage/StageConfigs")]
public class StageConfigs : ScriptableObject
{
    public List<StageConfig> all_stage_configs = new List<StageConfig>();
}
[System.Serializable]
public class StageConfig
{
    public int stage_id;
    public Mode mode;
    public int num_player;

    public StageConfig Clone()
    {
        return new StageConfig
        {
            stage_id = this.stage_id,
            mode = this.mode,
            num_player = this.num_player
        };
    }
}

