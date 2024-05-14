import { Line, Sunburst } from '@ant-design/charts';
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
import { useLocalStorage } from '@uidotdev/usehooks';
import { history, request } from '@umijs/max';
import { Button, Flex, message } from 'antd';
import { Typography } from 'antd/lib';
import React, { useEffect, useState } from 'react';
import {
  CalculateRequestDTO,
  PipeLayerDTO,
  Report,
  SunburstData,
} from 'typings';

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
  };

  debugger;
  const [lsReportID, _] = useLocalStorage<number>('reportID', 0);
  const [report, setReport] = useState<Report | undefined>(undefined);
  const [listDataSource, setListDataSource] = useState<
    {
      distance: number;
      temperature: number;
      fstMaterialName: string;
      scndMaterialName: string;
    }[]
  >([]);

  const [requestDTO, setRequestDTO] = useState<CalculateRequestDTO>();

  const [formRef] = ProForm.useForm<CalculateRequestDTO>();

  useEffect(() => {
    formRef.setFieldValue('precision', 1e-3);
  }, [formRef]);

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

  useEffect(() => {
    if (lsReportID == undefined || lsReportID == 0) return;

    request(`http://localhost:5114/api/HeatLoss/GetReport?id=${lsReportID}`)
      .then((report: Report) => {
        setReport(report);
        formRef.setFieldsValue({ ...report });
        formRef.setFieldValue('innerPipeRadius', report?.radiuses[0].value);

        const dto = formRef.getFieldsValue();
        handleFormValuesChanged(null, dto);
      })
      .catch(() => message.error('Отчет был удален или не существует'));
  }, []);

  const mapLayersToRadialData = (pipeLayers: PipeLayerDTO[]) => {
    const data: SunburstData = {
      value: {
        children: [],
      },
    };

    let distanceFromCenter = requestDTO!.innerPipeRadius / 10;
    let lastNode = data.value.children;
    for (const layer of pipeLayers) {
      distanceFromCenter += layer.width;
      lastNode.pop();
      lastNode.push({
        name: layer.materialID!,
        value: distanceFromCenter,
        children: [],
      });
      lastNode = lastNode[0].children;
    }

    return data;
  };

  const temperatureColsStructure: ProColumns[] = [
    {
      title: 'Удаление от центра трубы, м',
      renderText(_, record) {
        return record.distance.toFixed(3);
      },
    },
    {
      title: 'Температура, °C',
      renderText(_, record) {
        return record.temperature.toFixed(3);
      },
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

  const config = {
    data:
      requestDTO != undefined
        ? mapLayersToRadialData(requestDTO?.pipeLayers)
        : null,
    colorField: 'name',
    innerRadius: requestDTO?.innerPipeRadius / 5,
    radius:
      requestDTO?.pipeLayers.map((l) => l.width).reduce((acc, l) => acc + l) /
        5 +
      requestDTO?.innerPipeRadius / 5,
  };

  const handleFormValuesChanged = (_: any, dto: CalculateRequestDTO) => {
    if (
      dto == undefined ||
      dto.pipeLayers == undefined ||
      dto.pipeLayers.length == 0 ||
      dto.innerPipeRadius == undefined ||
      dto.innerPipeRadius == 0 ||
      dto.pipeLayers.filter(
        (l) => l.width == undefined || l.materialID == undefined,
      ).length > 0
    )
      return;

    setRequestDTO(dto);
  };

  return (
    <PageContainer
      ghost
      title="Калькулятор"
      footer={[
        <Button type="dashed" onClick={() => history.push('/previous')}>
          Просмотреть отчеты
        </Button>,
      ]}
    >
      <Flex vertical gap="2em">
        <ProCard
          style={{
            borderRadius: '2em',
            boxShadow: '0 0 1em gray',
            padding: '1em',
          }}
        >
          <Sunburst {...config} />
          <ProForm<CalculateRequestDTO>
            form={formRef}
            onFinish={onFormFinish}
            style={{ width: '100%' }}
            onValuesChange={handleFormValuesChanged}
          >
            <ProFormDigit
              name="innerPipeRadius"
              label="Внутренний радиус, м"
              required
              min={0}
              rules={[{ required: true }]}
              placeholder=""
            />
            <ProFormDigit
              name="pipeLength"
              label="Длина трубы, м"
              required
              min={0}
              rules={[{ required: true }]}
              placeholder=""
            />
            <ProFormDigit
              name="a1"
              label="Коэффициент теплоотдачи от горячего флюида к стенке"
              required
              min={0}
              rules={[{ required: true }]}
              placeholder=""
            />
            <ProFormDigit
              name="e"
              label="Степень черноты поверхности"
              required
              min={0}
              max={1}
              rules={[{ required: true }]}
              placeholder=""
            />
            <ProFormDigit
              name="innerTemp"
              label="Температура внутри трубы, °C"
              required
              min={0}
              rules={[{ required: true }]}
              placeholder=""
            />
            <ProFormDigit
              name="outterTemp"
              label="Температура окружающей среды, °C"
              required
              min={0}
              rules={[{ required: true }]}
              placeholder=""
            />
            <ProFormSelect
              name="precision"
              label="Точность вычислений"
              required
              rules={[{ required: true }]}
              options={[
                { label: 1, value: 1 },
                { label: '1E-1', value: 1e-1 },
                { label: '1E-2', value: 1e-2 },
                { label: '1E-3', value: 1e-3 },
              ]}
              placeholder=""
            />
            <ProFormList
              name="pipeLayers"
              style={{ width: '100%' }}
              creatorButtonProps={{
                position: 'bottom',
                creatorButtonText: 'новый слой',
              }}
              label="Слои стенки"
              max={6}
              min={2}
              deleteIconProps={{ tooltipText: '' }}
              copyIconProps={{ tooltipText: '' }}
            >
              {(field, index) => (
                <Flex gap={25} key={index} style={{ width: '100%' }}>
                  <ProFormDigit name={'width'} min={0} max={1} placeholder="" />
                  <ProFormTreeSelect
                    name={'materialID'}
                    request={async () => {
                      return await request(
                        'http://localhost:5114/api/HeatLoss/GetMaterialsForSelector',
                      );
                    }}
                    placeholder=""
                    fieldProps={{
                      style: {
                        minWidth: '200px',
                      },
                      dropdownStyle: {
                        width: '200px',
                      },
                    }}
                  />
                </Flex>
              )}
            </ProFormList>
            <ProFormSelect
              name="pipeOrientation"
              label="Расположение трубы"
              required
              rules={[{ required: true }]}
              options={[
                { label: 'Вертикальное', value: PipeOrientation.Vertical },
                { label: 'Горизонтальное', value: PipeOrientation.Horizontal },
              ]}
              placeholder=""
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
              <Typography.Title style={{ color: 'whitesmoke' }} level={3}>
                Report
              </Typography.Title>
              <Typography.Paragraph style={{ color: 'whitesmoke' }}>
                Heat loss by meter: {report?.ql.toFixed(3)}
              </Typography.Paragraph>
              <Typography.Paragraph style={{ color: 'whitesmoke' }}>
                Total heat loss: {report?.q.toFixed(3)}
              </Typography.Paragraph>
              <Typography.Paragraph style={{ color: 'whitesmoke' }}>
                Given heat transfer coefficient {'(a1)'}:{' '}
                {report?.a1.toFixed(3)}
              </Typography.Paragraph>
              <Typography.Paragraph style={{ color: 'whitesmoke' }}>
                Found heat transfer coefficient {'(a2)'}:{' '}
                {report?.a2.toFixed(3)}
              </Typography.Paragraph>
              <Typography.Paragraph style={{ color: 'whitesmoke' }}>
                Surface blackness: {report?.e}
              </Typography.Paragraph>
              <ProCard>
                <ProTable
                  rowKey={(row) => row.distance}
                  headerTitle={
                    <Typography.Title level={4}>Temperatures</Typography.Title>
                  }
                  dataSource={listDataSource}
                  search={false}
                  columns={temperatureColsStructure}
                />
              </ProCard>
              <Line
                data={listDataSource}
                xField="distance"
                yField="temperature"
                title="Падение температуры на слоях, °C"
                axis={{
                  x: {
                    title: 'Удаление от центра трубы, м',
                  },
                  y: {
                    title: 'Температура, °C',
                  },
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
