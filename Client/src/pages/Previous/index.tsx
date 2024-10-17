import CollapsedTemps from '@/components/CollapsedTemps';
import {
  PageContainer,
  ProCard,
  ProColumns,
  ProForm,
  ProFormSelect,
  ProList,
  ProTable,
} from '@ant-design/pro-components';
import { useLocalStorage } from '@uidotdev/usehooks';
import { history, request } from '@umijs/max';
import { Button, Divider, Flex, Form, List, Tag, Typography } from 'antd';
import { useForm } from 'antd/es/form/Form';
import dayjs from 'dayjs';
import { saveAs } from 'file-saver';
import { useEffect, useRef, useState } from 'react';
import { Report } from 'typings';

export default () => {
  const [_, setReportID] = useLocalStorage<number>('reportID', 0);
  const [reports, setReports] = useState<Report[]>([]);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [selectedRows, setSelectedRows] = useState<Report[]>([]);
  const [selectedCompParams, setSelectedCompParams] = useState<number[]>([-1]);

  const handleShowMore = (reportID: number) => {
    setReportID(reportID);
    history.push('/home');
  };

  const reportsColsStructure: ProColumns<Report>[] = [
    {
      title: 'Отчет от',
      renderText(_, record) {
        return dayjs(record.generatedAt).format('DD.MM.YY HH:mm');
      },
    },
    {
      title: 'Удельная теплопотеря (Qпог), Вт / м',
      dataIndex: 'ql',
    },
    {
      title: 'Полная теплопотеря (Q), Вт',
      dataIndex: 'q',
    },
    {
      title:
        'Коэффициент теплоотдачи от горячего флюида к стенке (α1), Вт/(м2·К)',
      dataIndex: 'a1',
    },
    {
      title:
        'Коэффициент теплоотдачи от стенки к холодному флюиду (α2), Вт/(м2·К)',
      dataIndex: 'a2',
    },
    {
      title: 'Степень черноты поверхнсти трубы (ε)',
      dataIndex: 'e',
    },
    {
      title: 'Температура горячего флюида, °C',
      dataIndex: 'innerTemp',
    },
    {
      title: 'Температура холодного флюида, °C',
      dataIndex: 'outterTemp',
    },
    {
      title: 'Рассчитанные температуры границ слоев, °C',
      width: '15%',
      render(_, report) {
        return <CollapsedTemps report={report} />;
      },
    },
    {
      width: 200,
      render(_, report) {
        return (
          <Flex vertical gap={10}>
            <Button type="default" onClick={() => saveReportAsXML(report.id)}>
              Сохранить как XML
            </Button>
            <Button type="default" onClick={() => handleShowMore(report.id)}>
              Подробнее
            </Button>
          </Flex>
        );
      },
    },
  ];

  const saveReportAsXML = (reportID: number) => {
    request(
      `/api/HeatLoss/GetXmlReport?requestID=${reportID}`,
      {
        method: 'POST',
        headers: {
          'Content-Type': 'application/xml',
        },
        responseType: 'blob',
      },
    ).then((file: Blob) =>
      saveAs(
        file,
        `Heat loss report ${dayjs(new Date()).format('DD.MM.YY HH:mm:ss')}.xml`,
      ),
    );
  };

  useEffect(() => {
    setIsLoading(true);
    request('/api/HeatLoss/GetReports', {
      method: 'POST',
      data: {},
    }).then((response: Report[]) => {
      setReports(response);
      setIsLoading(false);
    });

    setSelectedCompParams(comparingParams.map((param, i) => i));
  }, []);

  const prepareComparingParameter = (
    data: Report[],
    propSelector: (r: Report) => number,
    paramName: string,
    key: number,
  ) => {
    const propVals = data.map((r) => propSelector(r));
    const min = [...propVals].sort((a, b) => a - b)[0];
    const max = [...propVals].sort((a, b) => b - a)[0];

    const comparingDataItem: {
      key: number;
      paramName: string;
      values: {
        value: number;
        isBest: boolean;
        isWorst: boolean;
      }[];
    } = {
      key,
      paramName,
      values: propVals.map((v) => ({
        value: v,
        isBest: v == max,
        isWorst: v == min,
      })),
    };

    return comparingDataItem;
  };

  const comparingParams = [
    {
      propName: 'Удельная теплопотеря, Вт',
      selector: (report: Report) => report.ql,
    },
    {
      propName: 'Полная теплопотеря, Вт',
      selector: (report: Report) => report.q,
    },
    {
      propName: 'Коэфф. теплоотдачи от горяего флюида, Вт/(м²·К)',
      selector: (report: Report) => report.a1,
    },
    {
      propName: 'Коэфф. теплоотдачи к холодному флюиду, Вт/(м²·К)',
      selector: (report: Report) => report.a2,
    },
  ];

  const prepareComparingData = () => {
    const reports = selectedRows;

    const comparingData = comparingParams.map((param, i) =>
      prepareComparingParameter(reports, param.selector, param.propName, i)
    ).filter(p => selectedCompParams.includes(p.key));

    return comparingData;
  };

  return (
    <PageContainer
      title="Отчеты"
      footer={[
        <Button type="dashed" onClick={() => history.push('/home')}>
          Back to calculator
        </Button>,
      ]}
    >
      <ProCard
        style={{
          borderRadius: '2em',
          boxShadow: '0 0 1em gray',
          marginBottom: '2em',
          padding: '1em',
          height: '70em',
        }}
      >
        <ProTable
          pagination={{
            pageSize: 5,
          }}
          rowKey={(report) => report.id}
          rowSelection={{
            onChange(_, selectedRows) {
              setSelectedRows(selectedRows);
            },
          }}
          loading={isLoading}
          search={false}
          dataSource={reports.map((r) => ({
            ...r,
            ql: r.ql.toFixed(3),
            q: r.q.toFixed(3),
            a1: r.a1.toFixed(3),
            a2: r.a2.toFixed(3),
          }))}
          columns={reportsColsStructure}
          style={{
            overflowX: 'hidden',
            height: '100%',
          }}
          scroll={{
            y: '40em',
            x: '100%',
          }}
        />
      </ProCard>
      {selectedRows.length >= 2 && selectedRows.length <= 5 ? (
        <ProCard
          style={{
            borderRadius: '2em',
            boxShadow: '0 0 1em gray',
            padding: '1em',
          }}
        >
          <Flex justify="space-between" style={{ width: '100%' }}>
            <Typography.Title level={3}>
              Сравнение параметров отчетов
            </Typography.Title>
            <Flex vertical gap={10} align='end'>
              <Typography.Title level={5}>
                Параметры сравнения:
              </Typography.Title>
              <ProFormSelect
                style={{
                  maxWidth: '600px',
                  minWidth: '250px',
                }}
                mode="multiple"
                options={comparingParams.map((param, i) => ({
                  value: i,
                  label: param.propName,
                }))}

                fieldProps={{
                  value: selectedCompParams,
                  variant: 'filled'
                }}

                onChange={(params: number[]) => {
                  setSelectedCompParams(params);
                }}
              />
            </Flex>
          </Flex>
          <ProList<{
            paramName: string;
            values: {
              value: number;
              isBest: boolean;
              isWorst: boolean;
            }[];
          }>
            dataSource={prepareComparingData()}
            renderItem={(item) => {
              return (
                <List.Item>
                  <Flex vertical gap={10} style={{ width: '50%' }}>
                    <Typography.Title level={5}>
                      {item.paramName}
                    </Typography.Title>
                    <Flex justify="space-around" style={{ width: '100%' }}>
                      <Divider type="vertical" />
                      {item.values.map((val) => (
                        <>
                          <Typography.Text style={{ minWidth: 80 }}>
                            <Flex justify="center" align="center">
                              {val.isBest || val.isWorst ? (
                                <Tag color={val.isBest ? 'green' : 'red'}>
                                  {val.value}
                                </Tag>
                              ) : (
                                val.value
                              )}
                            </Flex>
                          </Typography.Text>
                          <Divider type="vertical" />
                        </>
                      ))}
                    </Flex>
                  </Flex>
                </List.Item>
              );
            }}
          />
        </ProCard>
      ) : (
        selectedRows.length > 5 ? (
          <Typography.Title>Выбрано слишком много записей</Typography.Title>
        ) : null
      )}
    </PageContainer>
  );
};
