﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MinerWarsMath;

namespace MinerWars.AppCode.Game.GUI.Core
{
    class MyRichLabelLine
    {
        private readonly float m_minLineHeight;

        private List<MyRichLabelPart> m_parts;
        private Vector2 m_size;

        public MyRichLabelLine(float minLineHeight)
        {
            m_minLineHeight = minLineHeight;

            m_parts = new List<MyRichLabelPart>(8);
            RecalculateSize();
        }

        public void AddPart(MyRichLabelPart part)
        {
            m_parts.Add(part);
            RecalculateSize();
        }

        public void ClearParts()
        {
            m_parts.Clear();
            RecalculateSize();
        }

        public IEnumerable<MyRichLabelPart> GetParts()
        {
            return m_parts;
        }

        private void RecalculateSize()
        {
            Vector2 newSize = new Vector2(0f, m_minLineHeight);
            foreach (MyRichLabelPart part in m_parts)
            {
                Vector2 partSize = part.GetSize();
                newSize.Y = Math.Max(partSize.Y, newSize.Y);
                newSize.X += partSize.X;
            }
            m_size = newSize;
        }

        /// <summary>
        /// Draws line
        /// </summary>
        /// <param name="position">Top-left position</param>
        /// <returns></returns>
        public bool Draw(Vector2 position)
        {
            Vector2 actualPosition = position;
            float centerY = position.Y + m_size.Y / 2f;
            foreach (MyRichLabelPart part in m_parts)
            {
                Vector2 partSize = part.GetSize();
                actualPosition.Y = centerY - partSize.Y / 2f;
                if (!part.Draw(actualPosition))
                {
                    return false;
                }
                actualPosition.X += partSize.X;
            }

            return true;
        }

        public Vector2 GetSize()
        {
            return m_size;
        }

        public bool IsEmpty()
        {
            return m_parts.Count == 0;
        }
    }
}
