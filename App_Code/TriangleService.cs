using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Serialization;

namespace TriangleWebService
{
    public class TriangleService : ITriangleService
    {
        public string GetCoordinates(string row, int col)
        {
            //NOTE: coordinates are given from top-left to bottom-right to match with web-side functionality for SVG coordinates
            var sRet = "";
            int rowIndex = char.ToUpper(row.ToCharArray()[0]) - 64; //get position of row letter
            if (((rowIndex >= 1) && (rowIndex <= 6)) && ((col >= 1) && (col <= 12)))  //check for valid entries
            {
                //top left (v2) coordinates of triangle
                int v2x = ((col - 1) / 2) * 10;
                int v2y = (rowIndex - 1) * 10;

                //bottom right (v3) coordinates of triangle
                int v3x = v2x + 10;
                int v3y = v2y + 10;

                //vertex opposite hypotenuse (v1) coordinates of triangle
                int v1x;
                int v1y;
                if (col % 2 == 1)
                {  //even or odd column
                   //odd column
                    v1x = v2x;
                    v1y = v2y + 10;
                }
                else
                {
                    //even column
                    v1x = v2x + 10;
                    v1y = v2y;
                }

                //send back Array of coordinates = [[v1x, v1y],[v2x, v2y],[v3x, v3y]];

                ArrayList alCoords = new ArrayList();
                //Array v1 = new Array[v1x, v1y];
                int[] arrV1 = { v1x, v1y };
                alCoords.Add(arrV1);
                int[] arrV2 = { v2x, v2y };
                alCoords.Add(arrV2);
                int[] arrV3 = { v3x, v3y };
                alCoords.Add(arrV3);

                JavaScriptSerializer jser = new JavaScriptSerializer();
                sRet = jser.Serialize(alCoords);
                //sRet = "['[0,10]','[0,0]','[10,10]']";
            }
            else
            {
                //row is not valid - add error message return parameter
            }

            return sRet;
        }

        public string GetRowColumn(int v1x, int v1y, int v2x, int v2y, int v3x, int v3y)
        {
            int column = 0;
            string row = "";

            //NOTE: we really only need v1x, v2x and v2y; but receive all coordinates for good measure

            //vertex 2 is the top-left of the triangle
            int colIndex = (v2x/10) + 1;  //now we need to know even or odd column
            //vertex 1 is opposite the hypotenuse and indicates left or right triangle (even or odd column)
            if (v1x == v2x)
            {
                //odd column
                column = (colIndex*2) - 1;
            }
            else
            {
                //even column
                column = (colIndex * 2);
            }

            //use top-left of triangle to find row
            int rowIndex = (v2y / 10);  //creates 0-based index of rows

            switch (rowIndex)
            {
                case 0:
                    row = "A";
                    break;
                case 1:
                    row = "B";
                    break;
                case 2:
                    row = "C";
                    break;
                case 3:
                    row = "D";
                    break;
                case 4:
                    row = "E";
                    break;
                case 5:
                    row = "F";
                    break;
            }

            return row + column.ToString();
        }
    }
}