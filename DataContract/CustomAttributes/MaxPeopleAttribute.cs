﻿namespace DataContract.CustomAttributes;

[AttributeUsage(AttributeTargets.All)]
public class MaxPeopleAttribute : Attribute
{
    public int MaxPeoples { get; }

    public MaxPeopleAttribute(int maxPeoples) => MaxPeoples = maxPeoples;
}

