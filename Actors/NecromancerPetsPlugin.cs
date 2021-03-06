﻿using System.Collections.Generic;
using System.Linq;
using Turbo.Plugins.Default;

namespace Turbo.Plugins.Jack.Actors
{
    public class NecromancerPetsPlugin : BasePlugin, IInGameWorldPainter
    {
        public HashSet<uint> SkeletonWarriorsIds { get; set; }
        public WorldDecoratorCollection SkeletonWarriorsDecorators { get; set; }
        public HashSet<uint> SkeletonMagesIds { get; set; }
        public WorldDecoratorCollection SkeletonMagesDecorators { get; set; }
        public HashSet<uint> SkeletonArchersIds { get; set; }
        public WorldDecoratorCollection SkeletonArchersDecorators { get; set; }


        public NecromancerPetsPlugin()
        {
            Enabled = true;
            SkeletonWarriorsIds = new HashSet<uint> { 473147, 473420, 473426, 473418, 473428, 473417 }; // ty SR ;p
            SkeletonMagesIds = new HashSet<uint> { 472275, 472588, 472769,  472606, 472715 };
            SkeletonArchersIds = new HashSet<uint> { 472801 };
        }

        public override void Load(IController hud)
        {
            base.Load(hud);
            var warriorBrush = Hud.Render.CreateBrush(222, 0, 255, 0, 2);
            SkeletonWarriorsDecorators = new WorldDecoratorCollection(
                new GroundShapeDecorator(hud)
                {
                    ShapePainter = WorldStarShapePainter.NewCross(Hud),
                    Radius = 1f,
                    Brush = warriorBrush,
                },
                new GroundCircleDecorator(hud)
                {
                    Radius = 1f,
                    Brush = warriorBrush,
                },
                new MapShapeDecorator(hud)
                {
                    ShapePainter = new TriangleShapePainter(hud),
                    Radius = 6f,
                    Brush = Hud.Render.CreateBrush(255, 0, 255, 0, 1),
                }
                //,
                //new MapShapeDecorator(hud)
                //{
                //    ShapePainter = new CrossShapeFilter(hud),
                //    Radius = 6f,
                //    BarBrush = Hud.Render.CreateBrush(255, 0, 255, 0, 1),
                //}
            );

            var mageBrush = Hud.Render.CreateBrush(178, 0, 255, 0, 2);
            SkeletonMagesDecorators = new WorldDecoratorCollection(
                new GroundShapeDecorator(hud)
                {
                    ShapePainter = WorldStarShapePainter.NewCross(Hud),
                    Radius = 0.65f,
                    Brush = mageBrush,
                },
                new GroundCircleDecorator(hud)
                {
                    Radius = 0.65f,
                    Brush = mageBrush,
                },
                new MapShapeDecorator(hud)
                {
                    ShapePainter = new CrossShapePainter(hud),
                    Radius = 1f,
                    Brush = mageBrush,
                }
            );
            foreach (var mapShapeDecorator in SkeletonMagesDecorators.GetDecorators<MapShapeDecorator>())
            {
                mapShapeDecorator.Enabled = false;
            }

            var archerBrush = Hud.Render.CreateBrush(178, 0, 255, 0, 2);
            SkeletonArchersDecorators = new WorldDecoratorCollection(
                new GroundShapeDecorator(hud)
                {
                    ShapePainter = WorldStarShapePainter.NewCross(Hud),
                    Radius = 0.65f,
                    Brush = archerBrush,
                },
                new GroundCircleDecorator(hud)
                {
                    Radius = 0.65f,
                    Brush = archerBrush,
                },
                new MapShapeDecorator(hud)
                {
                    ShapePainter = new CrossShapePainter(hud),
                    Radius = 1f,
                    Brush = archerBrush,
                }
            );
            foreach (var mapShapeDecorator in SkeletonArchersDecorators.GetDecorators<MapShapeDecorator>())
            {
                mapShapeDecorator.Enabled = false;
            }
        }

        public void PaintWorld(WorldLayer layer)
        {
            if (Hud.Render.UiHidden) return;
            if (Hud.Game.IsInTown) return;
            if (Hud.Game.Me.HeroClassDefinition.HeroClass != HeroClass.Necromancer) return;

            var warriors = Hud.Game.Actors.Where(a => a.SummonerAcdDynamicId == Hud.Game.Me.SummonerId && SkeletonWarriorsIds.Contains(a.SnoActor.Sno));
            foreach (var garg in warriors)
            {
                SkeletonWarriorsDecorators.Paint(layer, garg, garg.FloorCoordinate, null);
            }

            var mages = Hud.Game.Actors.Where(a => a.SummonerAcdDynamicId == Hud.Game.Me.SummonerId && SkeletonMagesIds.Contains(a.SnoActor.Sno));
            foreach (var zombieDog in mages)
            {
                SkeletonMagesDecorators.Paint(layer, zombieDog, zombieDog.FloorCoordinate, null);
            }

            var archers = Hud.Game.Actors.Where(a => a.SummonerAcdDynamicId == Hud.Game.Me.SummonerId && SkeletonArchersIds.Contains(a.SnoActor.Sno));
            foreach (var archer in archers)
            {
                SkeletonMagesDecorators.Paint(layer, archer, archer.FloorCoordinate, null);
            }
        }
    }
}