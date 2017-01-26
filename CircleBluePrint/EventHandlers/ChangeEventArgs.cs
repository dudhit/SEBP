using System;
using System.Windows.Media.Media3D;

namespace SoloProjects.Dudhit.SpaceEngineers.SEBP.EventArguments
{
    public class BlockChangeEventArgs : EventArgs
    {
        public Point3D hsv;
        public string blockSize;
        public string blockType;
        public BlockChangeEventArgs(Point3D colour, string size, string type)
        {
            this.hsv = colour;
            this.blockSize = size;
            this.blockType = type;
        }
    }

    public class FileChangeEventArgs : EventArgs
    {
        public string pathToSEfolder;
        public string steamName;
        public string steamId;
        public string bluePrintName;

        public FileChangeEventArgs(string pathToSEfolder, string steamName, string steamId, string bluePrintName)
        {
            this.pathToSEfolder = pathToSEfolder;
            this.steamName = steamName;
            this.steamId = steamId;
            this.bluePrintName = bluePrintName;
        }
    }

    //public class StartCalculatingArgs : EventArgs
    //{
    //    private bool GotSteamName;
    //    private bool GotSteamId;
    //    private bool GotSavePath;
    //    private bool GotBpName;

    //    public StartCalculatingArgs(bool GotSteamName, bool GotSteamId, bool GotSavePath, bool GotBpName)
    //    {
    //        this.GotSteamName = GotSteamName;
    //        this.GotSteamId = GotSteamId;
    //        this.GotSavePath = GotSavePath;
    //        this.GotBpName = GotBpName;
    //    }
    //}

    public class ShapeChangeEventArgs : EventArgs
    {
        public string shape;
        public double x;
        public double y;
        public double z;
        public ShapeChangeEventArgs(string what, double xAxis, double yAxis, double zAxis)
        {
            this.shape = what;
            this.x = xAxis;
            this.y = yAxis;
            this.z = zAxis;
        }

    }

    public class MainViewChangeEventArgs : EventArgs
    {
        public ShapeChangeEventArgs shape;
        public FileChangeEventArgs file;
        public BlockChangeEventArgs block;

        public MainViewChangeEventArgs(ShapeChangeEventArgs shape, FileChangeEventArgs file, BlockChangeEventArgs block)
        {
            this.shape = shape;
            this.file = file;
            this.block = block;
        }

    }

}
