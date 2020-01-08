using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearUiSpacing
{
    public float totalLength = 0.0F;
    public int partAmount = 0;
    public float partLength = 0.0F;
    public float offset = 0.0F;
    public float spacing = 0.0F;

    public LinearUiSpacing(float totalLength, float offset, float spacing, int partAmount)
    {
        this.partLength = ((totalLength - offset * 2.0F) - partAmount * spacing) / partAmount;

        this.totalLength = totalLength;
        this.partAmount = partAmount;
        this.offset = offset;
        this.spacing = spacing;
    }

    public LinearUiSpacing(float totalLength, float offset, float partLength, float minimumSpacing)
    {
        float workingSpace = totalLength - offset * 2;

        partAmount = (int)((workingSpace + minimumSpacing) / (partLength + minimumSpacing)); // TODO investigate floating point errors
        spacing = (workingSpace - (partLength * partAmount)) / (partAmount - 1);

        this.totalLength = totalLength;
        this.partLength = partLength;
        this.offset = offset;
    }

    public float GetNthPathPosition(int position)
    {
        return offset + (partLength + spacing) * position;
    }
}
