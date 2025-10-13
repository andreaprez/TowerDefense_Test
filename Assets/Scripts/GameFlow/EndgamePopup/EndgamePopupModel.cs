using TowerDefense.Utils;
using UnityEngine;

namespace TowerDefense.GameFlow.EndgamePopup
{
    public class EndgamePopupModel
    {
        public readonly Observable<string> Message = new("");
        public readonly Observable<Color> PanelColor = new(Color.white);
    }
}