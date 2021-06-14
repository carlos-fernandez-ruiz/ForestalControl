using System;
using System.Collections.Generic;
using System.Text;

namespace ControlForestal
{
    public class Drone
    {
        public struct Coords
        {
            public Coords(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; set; }
            public int Y { get; set; }
            public override string ToString() => $"{X} {Y}";
        }
        private enum Direction
        {
            N = 0,
            E = 1,
            S = 2,
            O = 3
        }
        private class LocationInfo
        {
            public Coords coordinates;
            public Direction direction;
            public override string ToString() => $"{coordinates.ToString()} {direction}";
        }

        LocationInfo oCurrentLocation;
        private Coords area;
        private string route;
        
        public Drone(int[] area, string startLocation, string route)
        {
            this.area.X = area[0];
            this.area.Y = area[1];
            this.route = route;
            this.oCurrentLocation = getStartLocation(startLocation);
        }

        private LocationInfo getStartLocation(string startLocationString)
        {
            LocationInfo oLocationInfo = new LocationInfo();
            startLocationString = startLocationString.Trim().Replace(" ", "");

            if (startLocationString.Length == 3)
            {
                int x;
                int y;
                if (int.TryParse(startLocationString[0].ToString(), out x) && int.TryParse(startLocationString[1].ToString(), out y))
                {
                    oLocationInfo.coordinates.X= x;
                    oLocationInfo.coordinates.Y = y;
                    //oLocationInfo.direction = startLocationString[2];
                    switch (startLocationString[2])
                    {
                        case 'N':
                            oLocationInfo.direction = Direction.N;
                            break;
                        case 'E':
                            oLocationInfo.direction = Direction.E;
                            break;
                        case 'S':
                            oLocationInfo.direction = Direction.S;
                            break;
                        case 'O':
                            oLocationInfo.direction = Direction.O;
                            break;
                    }
                } 
                else
                {
                    return null;
                }
            } 
            else
            {
                //Start position is not valid
                oLocationInfo = null;
            }
            return oLocationInfo;
        } 
        private bool isLocationValid(LocationInfo oLocation)
        {
            return oLocation.coordinates.X <= area.X && oLocation.coordinates.Y <= area.Y;
        }
        public string startRoute()
        {
            //Check if the start position is inside the starting area.
            if (oCurrentLocation != null && isLocationValid(oCurrentLocation))
            {
                foreach(char move in route.ToCharArray())
                {
                    LocationInfo nextLocation = getNextlocation(move);
                    if (isLocationValid(nextLocation))
                    {
                        oCurrentLocation = nextLocation;
                    } 
                    else
                    {
                        //TO DO
                        //Crear ruta para volver a la base
                        return "Reached limit of area. Last valid location: " + oCurrentLocation.ToString();
                    }
                }
                return oCurrentLocation.ToString();
            } 
            else
            {
                return "Starting location is not valid";
            }
        }

        private LocationInfo getNextlocation(char move)
        {
            LocationInfo nextLocation = oCurrentLocation;
            if (move == 'M')
            {
                switch (oCurrentLocation.direction)
                {
                    case Direction.N:
                        oCurrentLocation.coordinates.Y += 1;
                        break;
                    case Direction.S:
                        oCurrentLocation.coordinates.Y -= 1;
                        break;
                    case Direction.E:
                        oCurrentLocation.coordinates.X += 1;
                        break;
                    case Direction.O:
                        oCurrentLocation.coordinates.X -= 1;
                        break;
                }
            }
            else if (move == 'L')
            {
                if ( oCurrentLocation.direction - 1 < Direction.N)
                {
                    oCurrentLocation.direction = Direction.O;
                } 
                else
                {
                    oCurrentLocation.direction -= 1;
                }
            }
            else if (move == 'R')
            {
                if (oCurrentLocation.direction + 1 > Direction.O)
                {
                    oCurrentLocation.direction = Direction.N;
                }
                else
                {
                    oCurrentLocation.direction += 1;
                }
            }
            return nextLocation;
        }
    }
}
