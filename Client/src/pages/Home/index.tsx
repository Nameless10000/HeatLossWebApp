import {
  PageContainer,
  ProForm,
  ProFormDigit,
  ProFormList,
  ProFormSelect,
  ProFormTreeSelect,
} from '@ant-design/pro-components';
import { Checkbox, Flex } from 'antd';
import React, { useState } from 'react';
import { CalculateRequestDTO, PipeLayerDTO } from 'typings';
import styles from './index.less';
import { request } from '@umijs/max';

enum PipeOrientation {
  Vertical = 1,
  Horizontal = 2,
}

const formItemLayout = {
  labelCol: {
    xs: { span: 24 },
    sm: { span: 4 },
  },
  wrapperCol: {
    xs: { span: 24 },
    sm: { span: 20 },
  },
};

const formItemLayoutWithOutLabel = {
  wrapperCol: {
    xs: { span: 24, offset: 0 },
    sm: { span: 20, offset: 4 },
  },
};

const HomePage: React.FC = () => {
  const onFormFinish = (data: CalculateRequestDTO) => {
    /*request('http://localhost:5114/api/HeatLoss/CalcHeatLossReport', {
      method: 'POST',
      data,
    }).then((response: Report) => console.log({ response }));*/
    console.log({ data });
  };

  const [isMaterialsTemplate, setIsMaterialsTemplate] = useState<boolean[]>([
    false,
    false,
    false,
    false,
    false,
    false
  ]);

  const [layers, setLayers] = useState<PipeLayerDTO[]>();

  return (
    <PageContainer ghost title="Calculator">
      <Flex className={styles.container} dir="vertical" justify="center">
        <ProForm
          onFinish={onFormFinish}
          style={{ width: '80%' }}
          onValuesChange={(data) => console.log({ data })}
        >
          <ProFormDigit
            name="innerPipeRadius"
            label="Inner pipe radius, m"
            required
            min={0}
            rules={[{ required: true }]}
          />
          <ProFormDigit
            name="pipeLength"
            label="Pipe length, m"
            required
            min={0}
            rules={[{ required: true }]}
          />
          <ProFormDigit
            name="a1"
            label="Heat transfer coefficient"
            required
            min={0}
            rules={[{ required: true }]}
          />
          <ProFormDigit
            name="e"
            label="pipe surface blackness"
            required
            min={0}
            max={1}
            rules={[{ required: true }]}
          />
          <ProFormDigit
            name="innerTemp"
            label="Temperature inside pipe, °C"
            required
            min={0}
            rules={[{ required: true }]}
          />
          <ProFormDigit
            name="outterTemp"
            label="Temperature outside pipe, °C"
            required
            min={0}
            rules={[{ required: true }]}
          />
          <ProFormSelect
            name="precision"
            label="Precision, decimal signs"
            required
            rules={[{ required: true }]}
            options={[
              { label: 1, value: 1 },
              { label: '1E-1', value: 1e-1 },
              { label: '1E-2', value: 1e-2 },
              { label: '1E-3', value: 1e-3 },
            ]}
          />
          <ProFormList
            name="pipeLayers"
            style={{ width: '100%' }}
            creatorButtonProps={{
              position: 'bottom',
              creatorButtonText: 'layer',
            }}
            label="Pipe layers"
            max={6}
            min={2}
            deleteIconProps={{tooltipText: ''}}
            copyIconProps={{tooltipText: ''}}
          >
            {(field, index) => (
              <Flex gap={25} key={'pipeLayer'} style={{ width: '100%' }}>
              <ProFormDigit
                name={'width'}
                min={0}
                max={1}
                placeholder="layer width..."
              />
              <ProFormTreeSelect

                style={{width: '200px'}}
                name={'materialID'}
                fieldProps={{
                  width: '200px'
                }}
                request={async () => {
                  return await request(
                    'http://localhost:5114/api/HeatLoss/GetMaterials'
                  );
                }}
              />
            </Flex>
            )
            }
            
          </ProFormList>
          <ProFormSelect
            name="pipeOrientation"
            label="Pipe orientation"
            required
            rules={[{ required: true }]}
            options={[
              { label: 'Vertical', value: PipeOrientation.Vertical },
              { label: 'Horizontal', value: PipeOrientation.Horizontal },
            ]}
          />
        </ProForm>
      </Flex>
    </PageContainer>
  );
};

export default HomePage;
