using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Infrastructure.JoystickApi
{
    public static class JoyStickConstants
    {
        public static IDictionary<int,XboxButton> XboxButtonAxisDictIntKey { get; private set; }
        public static IDictionary<XboxButton, int> XboxButtonAxisDictEnumKey { get; private set; }

        public static IDictionary<int, XboxButton> XboxButtonButtonDictIntKey { get; private set; }
        public static IDictionary<XboxButton, int> XboxButtonButtonDictEnumKey { get; private set; }

        private static readonly ILog SLog = LogManager.GetLogger(typeof (JoyStickConstants));

        static JoyStickConstants()
        {
            CreateDictionary();
        }

        public static XboxButton GetAxisById(int id)
        {
            if (!XboxButtonAxisDictIntKey.ContainsKey(id))
            {
                SLog.ErrorFormat("Could not find Axis with id {0}",id);
                return XboxButton.Non;
            }
            return XboxButtonAxisDictIntKey[id];
        }

        public static int GetAxisByEnum(XboxButton btn)
        {
            if (!XboxButtonAxisDictEnumKey.ContainsKey(btn))
            {
                SLog.ErrorFormat("Could not find Axis with Key {0}", btn);
                return -1;
            }
            return XboxButtonAxisDictEnumKey[btn];
        }

        public static XboxButton GetButtonById(int id)
        {
            if (!XboxButtonButtonDictIntKey.ContainsKey(id))
            {
                SLog.ErrorFormat("Could not find Button with Id {0}", id);
                return XboxButton.Non;
            }
            return XboxButtonButtonDictIntKey[id];
        }

        public static int GetButtonByEnum(XboxButton btn)
        {
            if (!XboxButtonButtonDictEnumKey.ContainsKey(btn))
            {
                SLog.ErrorFormat("Could not find button with Key {0}", btn);
                return -1;
            }
            return XboxButtonButtonDictEnumKey[btn];
        }

        static void CreateDictionary()
        {
            XboxButtonAxisDictIntKey = new Dictionary<int, XboxButton>();
            XboxButtonAxisDictEnumKey = new Dictionary<XboxButton, int>();

            XboxButtonButtonDictIntKey = new Dictionary<int, XboxButton>();
            XboxButtonButtonDictEnumKey = new Dictionary<XboxButton, int>();

            #region Button
            XboxButtonButtonDictIntKey.Add(new KeyValuePair<int, XboxButton>(0 , XboxButton.A           ));
            XboxButtonButtonDictIntKey.Add(new KeyValuePair<int, XboxButton>(1 , XboxButton.B           ));
            XboxButtonButtonDictIntKey.Add(new KeyValuePair<int, XboxButton>(2 , XboxButton.Y           ));
            XboxButtonButtonDictIntKey.Add(new KeyValuePair<int, XboxButton>(3 , XboxButton.X           ));
            XboxButtonButtonDictIntKey.Add(new KeyValuePair<int, XboxButton>(4 , XboxButton.LB          ));
            XboxButtonButtonDictIntKey.Add(new KeyValuePair<int, XboxButton>(5 , XboxButton.RB          ));
            XboxButtonButtonDictIntKey.Add(new KeyValuePair<int, XboxButton>(8 , XboxButton.XButton     ));
            XboxButtonButtonDictIntKey.Add(new KeyValuePair<int, XboxButton>(7 , XboxButton.Start       ));
            XboxButtonButtonDictIntKey.Add(new KeyValuePair<int, XboxButton>(6 , XboxButton.Select      ));
            XboxButtonButtonDictIntKey.Add(new KeyValuePair<int, XboxButton>(11, XboxButton.XLeft       ));
            XboxButtonButtonDictIntKey.Add(new KeyValuePair<int, XboxButton>(12, XboxButton.XRight      ));
            XboxButtonButtonDictIntKey.Add(new KeyValuePair<int, XboxButton>(13, XboxButton.XUp         ));
            XboxButtonButtonDictIntKey.Add(new KeyValuePair<int, XboxButton>(14, XboxButton.XDown       ));
            XboxButtonButtonDictIntKey.Add(new KeyValuePair<int, XboxButton>(9 , XboxButton.L_JoyButton));
            XboxButtonButtonDictIntKey.Add(new KeyValuePair<int, XboxButton>(10, XboxButton.R_JoyButton));

            XboxButtonButtonDictEnumKey.Add(new KeyValuePair<XboxButton, int>(XboxButton.A, 0));
            XboxButtonButtonDictEnumKey.Add(new KeyValuePair<XboxButton, int>(XboxButton.B, 1));
            XboxButtonButtonDictEnumKey.Add(new KeyValuePair<XboxButton, int>(XboxButton.Y, 2));
            XboxButtonButtonDictEnumKey.Add(new KeyValuePair<XboxButton, int>(XboxButton.X, 3));
            XboxButtonButtonDictEnumKey.Add(new KeyValuePair<XboxButton, int>(XboxButton.LB, 4));
            XboxButtonButtonDictEnumKey.Add(new KeyValuePair<XboxButton, int>(XboxButton.RB, 5));
            XboxButtonButtonDictEnumKey.Add(new KeyValuePair<XboxButton, int>(XboxButton.XButton, 8));
            XboxButtonButtonDictEnumKey.Add(new KeyValuePair<XboxButton, int>(XboxButton.Start, 7));
            XboxButtonButtonDictEnumKey.Add(new KeyValuePair<XboxButton, int>(XboxButton.Select, 6));
            XboxButtonButtonDictEnumKey.Add(new KeyValuePair<XboxButton, int>(XboxButton.XLeft, 11));
            XboxButtonButtonDictEnumKey.Add(new KeyValuePair<XboxButton, int>(XboxButton.XRight, 12));
            XboxButtonButtonDictEnumKey.Add(new KeyValuePair<XboxButton, int>(XboxButton.XUp, 13));
            XboxButtonButtonDictEnumKey.Add(new KeyValuePair<XboxButton, int>(XboxButton.XDown, 14));
            XboxButtonButtonDictEnumKey.Add(new KeyValuePair<XboxButton, int>(XboxButton.L_JoyButton, 9));
            XboxButtonButtonDictEnumKey.Add(new KeyValuePair<XboxButton, int>(XboxButton.R_JoyButton, 10));
            #endregion

            #region Axis
            XboxButtonAxisDictIntKey.Add(new KeyValuePair<int, XboxButton>(0, XboxButton.L_JoyLeftRight));
            XboxButtonAxisDictIntKey.Add(new KeyValuePair<int, XboxButton>(1, XboxButton.L_JoyUpDown    ));
            XboxButtonAxisDictIntKey.Add(new KeyValuePair<int, XboxButton>(3, XboxButton.R_JoyLeftRight));
            XboxButtonAxisDictIntKey.Add(new KeyValuePair<int, XboxButton>(4, XboxButton.R_JoyUpDown    ));
            XboxButtonAxisDictIntKey.Add(new KeyValuePair<int, XboxButton>(4, XboxButton.LT             ));
            XboxButtonAxisDictIntKey.Add(new KeyValuePair<int, XboxButton>(2, XboxButton.RT             ));

            XboxButtonAxisDictEnumKey.Add(new KeyValuePair<XboxButton, int>(XboxButton.L_JoyLeftRight, 0));
            XboxButtonAxisDictEnumKey.Add(new KeyValuePair<XboxButton, int>(XboxButton.L_JoyUpDown, 1));
            XboxButtonAxisDictEnumKey.Add(new KeyValuePair<XboxButton, int>(XboxButton.R_JoyLeftRight, 3));
            XboxButtonAxisDictEnumKey.Add(new KeyValuePair<XboxButton, int>(XboxButton.R_JoyUpDown, 4));
            XboxButtonAxisDictEnumKey.Add(new KeyValuePair<XboxButton, int>(XboxButton.LT, 4));
            XboxButtonAxisDictEnumKey.Add(new KeyValuePair<XboxButton, int>(XboxButton.RT, 2));


            #endregion

        }

    }


    public enum XboxButton
    {
        Non,
        A,B,X,Y,
        RT,
        RB,
        LT,
        LB,
        XLeft,
        XRight,
        XDown,
        XUp,
        R_JoyLeftRight,
        R_JoyUpDown,
        R_JoyButton,
        L_JoyLeftRight,
        L_JoyUpDown,
        L_JoyButton,
        Start,
        Select,
        XButton

    }

}
