using FurryCutter.Gameplay.Session.ServiceLocator;

namespace UnitySpriteCutter.CutPostProcessing
{
    public interface ICutPostProcessorsHolder : ISessionService
    {
        void AddPostProcessor<T>(ICutPostProcessor postProcessor) where T : ICutPostProcessor;
        T GetPostProcessor<T>() where T : ICutPostProcessor;
        bool HasPostProcessor<T>() where T : ICutPostProcessor;
    }
}