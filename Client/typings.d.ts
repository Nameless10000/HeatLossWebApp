import '@umijs/max/typings';
import { Dayjs } from 'dayjs';
export type Dayjs = Dayjs;

export type MaterialGroup = {
  name: string;
  id: number;
  materialsCount? : number,
  message?: string
};

export type Material = {
  aCoeff: number;
  bCoeff: number;
  cCoeff: number;
  name: string;
  id: number;
  materialGroupID: number;
  materialGroup: MaterialGroup;
};

export type NumericDB = {
  value: number;
  id: number;
  reportID: number;
};

export type PipeLayer = {
  id: number;
  reportID: number;
  materialID: number | undefined;
  material: Material;
  width: number;
};

export type Report = {
  id: number;
  generatedAt: Date;
  q: number;
  innerQl: number;
  outterQl: number;
  a1: number;
  a2: number;
  e: number;
  ql: number;
  innerTemp: number;
  outterTemp: number;
  temperatures: NumericDB[];
  radiuses: NumericDB[];
  pipeLayers: PipeLayer[];
  criticalDiameter: number;
};

export enum PipeOrientation {
  Vertical = 1,
  Horizontal = 2,
}

export type PipeLayerDTO = {
  materialID: number | undefined;
  materialGroupID: number | undefined;
  width: number;
  aCoeff: number | undefined;
  bCoeff: number | undefined;
  cCoeff: number | undefined;
  materialName: string | undefined;
  layerKey: number;
};

export type CalculateRequestDTO = {
  innerPipeRadius: number;
  pipeLength: number;
  pipeLayers: PipeLayerDTO[];
  a1: number;
  e: number;
  innerTemp: number;
  outterTemp: number;
  precision: number;
  pipeOrientation: PipeOrientation;
};

export type MaterialDTO = {
  aCoeff: number;
  bCoeff: number;
  cCoeff: number;
  name: string;
  materialGroupID: number | undefined;
};

export type MaterialGroupDTO = {
  name: string;
}

export type SunburstData = {
  value: {
    children: SunBurstDataItem[]
  }
}

export type SunBurstDataItem = {
  value: string | number;
  name: number;
  children: SunBurstDataItem[]
}