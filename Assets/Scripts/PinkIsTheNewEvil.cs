using UnityEngine;

public class PinkIsTheNewEvil : MonoBehaviour {
	public static MainSystems MainSystems;
	public static PlayerController PlayerController;
	public static CameraLogic CameraLogic;
	public static EnemySpawner EnemySpawner;
	public static ReflectionProbe ReflectionProbe;

	[SerializeField] private MainSystems mainSystems;
	[SerializeField] private PlayerController playerController;
	[SerializeField] private CameraLogic cameraLogic;
	[SerializeField] private EnemySpawner enemySpawner;
	[SerializeField] private ReflectionProbe reflectionProbe;

	void Start () {
		MainSystems = mainSystems;
		PlayerController = playerController;
		CameraLogic = cameraLogic;
		EnemySpawner = enemySpawner;
		ReflectionProbe = reflectionProbe;
	}
}
