import { ProFormDigit, ProFormSelect } from '@ant-design/pro-components';
import { request } from '@umijs/max';
import { Button, Flex, Form } from 'antd';
import { useState } from 'react';
import { Material, MaterialGroup, PipeLayerDTO } from 'typings';

export type LayersListProps = {
  name: string;
};

export default (props: LayersListProps) => {
  const [layers, setLayers] = useState<PipeLayerDTO[]>([]);
  const [groups, setGroups] = useState<MaterialGroup[]>([]);
  const [materials, setMaterials] = useState<{}>();
  let [layersCount, setCount] = useState<number>(1);

  const handleAddLayer = () => {
    const newLayer: PipeLayerDTO = {
      materialID: undefined,
      materialGroupID: undefined,
      width: 0,
      aCoeff: undefined,
      bCoeff: undefined,
      cCoeff: undefined,
      materialName: undefined,
      layerKey: new Number(layersCount).valueOf(),
    };
    setCount((prev) => prev + 1);
    setLayers((prev) => [...prev, newLayer]);
  };

  const handleLayerPropChanged = (data) => {
    console.log(data.width);
  };

  const mapLayer = (pipeLayer: PipeLayerDTO) => {
    return (
      <Form width="100%" onValuesChange={handleLayerPropChanged}>
        <Flex gap={10}>
          <ProFormDigit required label="Width" name="width" />

          <ProFormSelect
            name="materialID"
            width={'1000'}
            request={async () => {
              const resp = (
                (await request(
                  'http://localhost:5114/api/HeatLoss/GetMaterials',
                )) as Material[]
              ).map((m) => ({ label: m.name, value: m.id }));
              return resp;
            }}
          />
          <Button
            type="primary"
            onClick={() => {
              debugger;
              setLayers((prev) =>
                prev.filter((l) => l.layerKey != pipeLayer.layerKey),
              );
            }}
          >
            Remove
          </Button>
        </Flex>
      </Form>
    );
  };

  return (
    <>
      <Flex vertical>{layers.map(mapLayer)}</Flex>
      <Button type="primary" onClick={handleAddLayer}>
        Add layers
      </Button>
    </>
  );
};
