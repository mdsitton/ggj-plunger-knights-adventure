public interface IStateSystem
{
    (AiState nextState, float delayTime) OnIdleState(AiState previousState);
    (AiState nextState, float delayTime) OnSearchState(AiState previousState);
    (AiState nextState, float delayTime) OnAttackState(AiState previousState);
    (AiState nextState, float delayTime) OnDeadState(AiState previousState);
}