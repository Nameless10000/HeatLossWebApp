/*import { useEffect, useRef } from "react";

export type PipeViewProps = {
    radiuses: number[];
    colors: string[];
}

export default ({radiuses, colors}: PipeViewProps) => {

    const canvas = useRef<HTMLCanvasElement>(null);
    
    useEffect(() => {
        const ctx = canvas.current?.getContext("2d")!;

        drawPipeChart(ctx, radiuses, colors);
    }, [])

    const drawPipeChart = (ctx: CanvasRenderingContext2D, radiuses: number[], colors: string[]) => {
        // Очистка холста
        ctx.clearRect(0, 0, ctx.canvas.width, ctx.canvas.height);
      
        // Расчет размеров
        const centerX = ctx.canvas.width / 2;
        const centerY = ctx.canvas.height / 2;
        const pipeWidth = Math.min(...radiuses) / 2;
      
        // Отрисовка окружностей
        for (let i = 0; i < radiuses.length; i++) {
          const radius = radiuses[i];
          const startAngle = -Math.PI;
          const endAngle = Math.PI;
      
          ctx.beginPath();
          ctx.arc(centerX, centerY, radius, startAngle, endAngle, false);
          ctx.lineWidth = 0;
          ctx.strokeStyle = 'black';
          ctx.stroke();
      
          ctx.fillStyle = colors[i];
          ctx.fill();
        }
      };

      return <>
        <canvas ref={canvas}/>
      </>
}*/

import { useEffect, useRef, useState } from "react";

export type PipeViewProps = {
  radiuses: number[];
  colors: string[];
};

export default ({ radiuses, colors }: PipeViewProps) => {
  const canvas = useRef<HTMLCanvasElement>(null);
  const [hoveredIndex, setHoveredIndex] = useState(-1);

  useEffect(() => {
    const ctx = canvas.current?.getContext("2d")!;

    const handleMouseMove = (event: MouseEvent) => {
      const rect = canvas.current!.getBoundingClientRect();
      const x = event.clientX - rect.left;
      const y = event.clientY - rect.top;

      const newHoveredIndex = findHoveredCircle(ctx, x, y, radiuses);
      setHoveredIndex(newHoveredIndex);
    };

    canvas.current?.addEventListener("mousemove", handleMouseMove);

    return () => canvas.current?.removeEventListener("mousemove", handleMouseMove);
  }, [radiuses, colors]);

  const drawPipeChart = (ctx: CanvasRenderingContext2D, radiuses: number[], colors: string[]) => {
    // ... existing drawing logic ...

    // Highlight hovered circle
    if (hoveredIndex !== -1) {
      ctx.lineWidth = 2;
      ctx.strokeStyle = "white";
      ctx.stroke();
    }
  };

  const findHoveredCircle = (ctx: CanvasRenderingContext2D, x: number, y: number, radii: number[]) => {
    const centerX = ctx.canvas.width / 2;
    const centerY = ctx.canvas.height / 2;

    for (let i = 0; i < radii.length; i++) {
      const radius = radii[i];
      const distance = Math.sqrt(Math.pow(x - centerX, 2) + Math.pow(y - centerY, 2));
      if (distance <= radius) {
        return i;
      }
    }

    return -1;
  };

  return (
    <>
      <canvas ref={canvas}/>
    </>
  );
};