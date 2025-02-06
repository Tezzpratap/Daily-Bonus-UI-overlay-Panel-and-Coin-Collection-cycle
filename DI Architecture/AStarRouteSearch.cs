using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRouteSearch
{
}

public class AStarRouteSearch : IRouteSearch
{
}

public class CharacterService
{
    readonly IRouteSearch routeSearch;

    public CharacterService(IRouteSearch routeSearch)
    {
        this.routeSearch = routeSearch;
    }
}

public class ActorsView : MonoBehaviour
{
}
