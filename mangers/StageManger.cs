using System.Collections.Generic;
using ToeJamEarl;

public class StageManager
{
    private int currentStageIndex = 0;

    private readonly List<StageCriteria> stages = new List<StageCriteria>
    {
        new StageCriteria { HasSand = true, HasWater = true, EnemyTypesRequired = 3 },
        new StageCriteria { HasHighway = true, HasWater = true, EnemyTypesRequired = 3 }
    };

    public StageCriteria GetCurrentCriteria() => stages[currentStageIndex];

    public void AdvanceStage()
    {
        currentStageIndex = (currentStageIndex + 1) % stages.Count;
    }
}