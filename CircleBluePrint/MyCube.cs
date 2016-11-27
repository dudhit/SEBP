using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleBluePrint
{
    class MyCube
    {
        //floats for grid position
        private float x;
        private float y;
        private float z;

        public MyCube()
            : this(1F, 1F, 1F)
        { }
        public MyCube(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;

        }
        public MyCube(double x, double y, double z)
        {
            this.x = (float)x;
            this.y = (float)y;
            this.z = (float)z;

        }

        public float X()
        {
            return x;
        }
        public float Y()
        {
            return y;
        }
        public float Z()
        {
            return z;
        }
        public override string ToString()
        {
            string result;
            StringBuilder sb = new StringBuilder();
      
                sb.Append("x=");
                sb.Append(X());
                sb.Append(",");
                sb.Append("y=");
                sb.Append(Y());
                sb.Append(",");
                sb.Append("z=");
                sb.Append(Z());
                result = sb.ToString();
         
            return result;
        }
        /* 
         * <MyObjectBuilder_CubeBlock xsi:type="MyObjectBuilder_CubeBlock">
              <SubtypeName>LargeHeavyBlockArmorBlock</SubtypeName>
              <Min x="-1" y="0" z="0" />
              <BuiltBy>144115188075855877</BuiltBy>
            </MyObjectBuilder_CubeBlock>
         */

    }
}
