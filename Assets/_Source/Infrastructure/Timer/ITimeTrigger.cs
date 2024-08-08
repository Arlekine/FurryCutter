using System;

public interface ITimeTrigger
{
    event Action Triggered;
}