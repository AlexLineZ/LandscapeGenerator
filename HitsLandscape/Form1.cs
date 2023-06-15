using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HitsLandscape.Disaster;
using HitsLandscape.NoiseGeneration;

namespace HitsLandscape
{
    public partial class Form1 : Form
    {
        private GameModel gameModel;
        
        private int cellSize;
        private int visibleSize = 100;
        private int offsetX = 0;
        private int offsetY = 0;
        private int stepSize = 10;
        
        private string selectedBlock = "";
        private Point hoveredCell = new Point(-1, -1);
        private SolidBrush brush = new SolidBrush(Color.White);
        public Form1()
        {
            InitializeComponent();
            this.SizeChanged += FormSizeChanged;
            this.MouseWheel += Form1_MouseWheel;

            foreach (RadioButton radioButton in groupBox1.Controls.OfType<RadioButton>())
            {
                radioButton.CheckedChanged += RadioButton_CheckedChanged;
            }
            
            foreach (Button disasterButton in groupBox2.Controls.OfType<Button>())
            {
                disasterButton.Click += DisasterButton_Clicked;
            }

            gameModel = new GameModel();
            gameModel.MapUpdated += GameModel_MapUpdated;
        }

        private void GenerateButtonClick(object sender, EventArgs e)
        {
            if (sizeTextBox.Text != "")
            {
                int size = int.Parse(sizeTextBox.Text);
                
                string selectedNoise = noiseComboBox.SelectedItem.ToString();
                NoiseType noiseType = Translator.GetNoiseTypeFromString(selectedNoise);
                
                gameModel.CreateMap(size, noiseType);
            }
            Refresh();
        }
        
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;

            if (radioButton.Checked)
            {
                selectedBlock = radioButton.Text;
            }
        }
        
        private void DisasterButton_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            DisasterType selectedDisaster = Translator.GetSelectedDisasterType(button.Text);

            gameModel.GetDisaster(selectedDisaster);
            RefreshChangedCells();
        }
        
        
        private void FormSizeChanged(object sender, EventArgs e)
        {
            CalculateCellSize();
            Refresh();
        }

        private void GameModel_MapUpdated(object sender, EventArgs e)
        {
            RefreshChangedCells();
        }

        private void RefreshChangedCells()
        {
            List<Cell> changedCells = gameModel.GetChangedCells();

            if (changedCells.Count > 0)
            {
                foreach (Cell cell in changedCells.Where(cell => cell != null).ToList())
                {
                    int x = cell.GetX();
                    int y = cell.GetY();

                    Rectangle cellRect = new Rectangle(x * cellSize, y * cellSize, cellSize, cellSize);
                    Invalidate(cellRect);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var map = gameModel.GetMap();
            
            if (map != null)
            {
                Graphics g = e.Graphics;
                cellSize = CalculateCellSize();

                DrawMap(g);
            }
        }

        private void DrawMap(Graphics g)
        {
            var map = gameModel.GetMap();

            int startX = offsetX;
            int startY = offsetY;
            int endX = Math.Min(startX + visibleSize, map.Size);
            int endY = Math.Min(startY + visibleSize, map.Size);

            for (int y = startY; y < endY; y++)
            {
                for (int x = startX; x < endX; x++)
                {
                    Color color = map.GetCell(x, y).GetProperty().Display();

                    brush.Color = color;
                    Rectangle rect = new Rectangle((x - startX) * cellSize, (y - startY) * cellSize, cellSize, cellSize);
                    g.FillRectangle(brush, rect);

                    if (x == hoveredCell.X && y == hoveredCell.Y)
                    {
                        Pen pen = new Pen(Color.Black, 1);
                        g.DrawRectangle(pen, rect);
                    }
                }
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            var map = gameModel.GetMap();
            int x = (e.X / (cellSize > 0 ? cellSize : 1)) + offsetX;
            int y = (e.Y / (cellSize > 0 ? cellSize : 1)) + offsetY;

            if (map != null && x >= 0 && x < map.Size && y >= 0 && y < map.Size && selectedBlock != "")
            {
                CellType selectedCellType = Translator.GetSelectedCellType(selectedBlock);
                gameModel.UpdateCell(x, y, selectedCellType);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            var map = gameModel.GetMap();

            if (map != null)
            {
                int x = (e.X / cellSize) + offsetX;
                int y = (e.Y / cellSize) + offsetY;

                if (x >= 0 && x < map.Size && y >= 0 && y < map.Size)
                {
                    if (hoveredCell.X != x || hoveredCell.Y != y)
                    {
                        Rectangle oldHoveredCellRect = new Rectangle((hoveredCell.X - offsetX) * cellSize, (hoveredCell.Y - offsetY) * cellSize, cellSize, cellSize);
                        hoveredCell = new Point(x, y);
                        Rectangle newHoveredCellRect = new Rectangle((hoveredCell.X - offsetX) * cellSize, (hoveredCell.Y - offsetY) * cellSize, cellSize, cellSize);

                        Invalidate(oldHoveredCellRect);
                        Invalidate(newHoveredCellRect);
                    }
                }
            }
        }

        private int CalculateCellSize()
        {
            if (gameModel.GetMap() == null || gameModel.GetMap().Size <= 0)
                return 0;

            cellSize = ClientSize.Height / Math.Max(1, visibleSize);
            return cellSize;
        }

        private void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            int delta = e.Delta;

            if (delta > 0 && visibleSize < gameModel.GetMap().Size)
            {
                visibleSize += stepSize;
                CalculateCellSize();
                Refresh();
            }
            else if (delta < 0 && visibleSize > 1)
            {
                visibleSize -= stepSize;
                CalculateCellSize();
                Refresh();
            }
        }
    }
}
