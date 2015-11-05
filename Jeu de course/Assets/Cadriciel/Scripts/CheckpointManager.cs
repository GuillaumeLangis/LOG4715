using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CheckpointManager : MonoBehaviour 
{

	[SerializeField]
	private GameObject _carContainer;

    [SerializeField] Transform checkPointRoot;
	public int checkpointcount
    {
        get
        {
            return checkPointRoot.childCount;
        }
    }
	[SerializeField]
	private int _totalLaps;

	private bool _finished = false;
	
	private Dictionary<CarController,PositionData> _carPositions = new Dictionary<CarController, PositionData>();
    [SerializeField] List<CarController> orderedCars = new List<CarController>();
    public int GetPositionInRace(CarController source)
    {
        OrderCars();
        return orderedCars.FindIndex(x => x == source);
    }
    public CarController GetNextCar(CarController source)
    {
        OrderCars();
        for (int i = 0; i < orderedCars.Count; i++)
        {
            if (orderedCars[i] == source)
            {
                if (i != 0)
                    return orderedCars[i - 1];
            }
        }

        return null;
    }
    public CarController GetFirstCar()
    {
        OrderCars();
        return orderedCars[0];
    }

	private class PositionData
	{
		public int lap;
		public int checkPoint;
		public int position;
	}

	// Use this for initialization
	void Awake () 
	{
		foreach (CarController car in _carContainer.GetComponentsInChildren<CarController>(true))
		{
			_carPositions[car] = new PositionData();
            car.checkpointManager = this;
            orderedCars.Add(car);
		}
	}

    void OrderCars()
    {
        var query = _carPositions.OrderByDescending(x => x.Value.lap).ThenByDescending(x => x.Value.checkPoint).ThenBy(x => x.Value.position);
        orderedCars.Clear();
        foreach (var car in query)
        {
            orderedCars.Add(car.Key);
        }
    }
	
	public void CheckpointTriggered(CarController car, int checkPointIndex)
	{

		PositionData carData = _carPositions[car];

		if (!_finished)
		{
			if (checkPointIndex == 0)
			{
				if (carData.checkPoint == checkpointcount-1)
				{
					carData.checkPoint = checkPointIndex;
					carData.lap += 1;
					Debug.Log(car.name + " lap " + carData.lap);
					if (IsPlayer(car))
					{
						GetComponent<RaceManager>().Announce("Tour " + (carData.lap+1).ToString());
					}

					if (carData.lap >= _totalLaps)
					{
						_finished = true;
						GetComponent<RaceManager>().EndRace(car.name.ToLower());
					}
				}
			}
			else if (carData.checkPoint == checkPointIndex-1) //Checkpoints must be hit in order
			{
				carData.checkPoint = checkPointIndex;
			}
		}


	}

	bool IsPlayer(CarController car)
	{
		return car.GetComponent<CarUserControlMP>() != null;
	}
}
