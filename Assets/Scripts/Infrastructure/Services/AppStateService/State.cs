namespace CodeBase.Infrastructure.Services
{
    public enum State
    {
        None = 0,
        BootstrapState = 1,
        BuildingTurretState = 2,
        GameLoopState = 3,
        MenuState = 4,
        PauseState = 5,
        PlayingIdleState = 6,
    }
}
