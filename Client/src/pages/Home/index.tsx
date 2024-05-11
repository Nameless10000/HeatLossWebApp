import { Line } from '@ant-design/charts';
import type { ProColumns } from '@ant-design/pro-components';
import {
  PageContainer,
  ProCard,
  ProForm,
  ProFormDigit,
  ProFormList,
  ProFormSelect,
  ProFormTreeSelect,
  ProTable,
} from '@ant-design/pro-components';
import { request } from '@umijs/max';
import { Button, Flex } from 'antd';
import { Typography } from 'antd/lib';
import React, { useEffect, useState } from 'react';
import { CalculateRequestDTO, Report } from 'typings';
import { history } from '@umijs/max';

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
    request('http://localhost:5114/api/HeatLoss/CalcHeatLossReport', {
      method: 'POST',
      data,
    }).then((response: Report) => setReport(response));
    // Ебануть табличку под расчеты, пару диаграмм (температура и радиусы)
    // ответ от серваки приходит, все четко.
    // создать 2 страницы под просмотр проведенных расчетов из БД
  };

  const [report, setReport] = useState<Report | undefined>(undefined);
  const [listDataSource, setListDataSource] = useState<
    {
      distance: number;
      temperature: number;
      fstMaterialName: string;
      scndMaterialName: string;
    }[]
  >([]);

  useEffect(() => {
    if (report == undefined) return;

    setListDataSource(
      report.temperatures.map((temp, i) => ({
        distance: report.radiuses[i].value,
        temperature: temp.value,
        fstMaterialName: i > 0 ? report.pipeLayers[i - 1].material.name : '-',
        scndMaterialName:
          i + 1 < report.temperatures.length
            ? report.pipeLayers[i].material.name
            : '-',
      })),
    );
  }, [report]);

  const temperatureColsStructure: ProColumns[] = [
    {
      title: 'Удаление от центра трубы, м',
      dataIndex: 'distance',
    },
    {
      title: 'Температура, °C',
      dataIndex: 'temperature',
    },
    {
      title: 'Материал слоя слева',
      dataIndex: 'fstMaterialName',
    },
    {
      title: 'Материал слоя справа',
      dataIndex: 'scndMaterialName',
    },
  ];

  return (
    <PageContainer ghost title="Calculator" footer={([
      <Button type='dashed' onClick={() => history.push('/previous')}>Previous reports</Button>
    ])}>
      <Flex vertical gap="2em">
        <ProCard
          style={{
            borderRadius: '2em',
            boxShadow: '0 0 1em gray',
            padding: '1em',
          }}
        >
          <ProForm
            onFinish={onFormFinish}
            style={{ width: '100%' }}
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
              deleteIconProps={{ tooltipText: '' }}
              copyIconProps={{ tooltipText: '' }}
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
                    style={{ width: '200px' }}
                    name={'materialID'}
                    fieldProps={{
                      width: '200px',
                    }}
                    request={async () => {
                      return await request(
                        'http://localhost:5114/api/HeatLoss/GetMaterials',
                      );
                    }}
                  />
                </Flex>
              )}
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
        </ProCard>
        {report != undefined ? (
          <ProCard
            style={{
              borderRadius: '2em',
              boxShadow: '0 0 1em gray',
              padding: '1em',
            }}
          >
            <Flex vertical>
              <Typography.Title style={{color: 'whitesmoke'}} level={3}>Report</Typography.Title>
              <Typography.Paragraph style={{color: 'whitesmoke'}}>
                Heat loss by meter: {report?.ql}
              </Typography.Paragraph>
              <Typography.Paragraph style={{color: 'whitesmoke'}}>
                Total heat loss: {report?.q}
              </Typography.Paragraph >
              <Typography.Paragraph style={{color: 'whitesmoke'}}>
                Given heat transfer coefficient {'(a1)'}: {report?.a1}
              </Typography.Paragraph>
              <Typography.Paragraph style={{color: 'whitesmoke'}}>
                Found heat transfer coefficient {'(a2)'}: {report?.a2}
              </Typography.Paragraph>
              <Typography.Paragraph style={{color: 'whitesmoke'}}>
                Surface blackness: {report?.e}
              </Typography.Paragraph>
              <ProCard>
                <ProTable
                rowKey={(row) => row.distance}
                headerTitle={(
                  <Typography.Title level={4}>Temperatures</Typography.Title>
                )}
                  dataSource={listDataSource}
                  search={false}
                  columns={temperatureColsStructure}
                />
              </ProCard>
              <Line
              data={listDataSource}
              xField='distance'
              yField='temperature'
              title='Падение температуры на слоях, °C'
              axis={{
                x: {
                  title: 'Удаление от центра трубы, м'
                },
                y: {
                  title: 'Температура, °C'
                }
              }}  
              />
            </Flex>
          </ProCard>
        ) : null}
      </Flex>
    </PageContainer>
  );
};
export default HomePage;
