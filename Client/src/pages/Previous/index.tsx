import CollapsedTemps from '@/components/CollapsedTemps';
import {
  PageContainer,
  ProCard,
  ProColumns,
  ProTable,
} from '@ant-design/pro-components';
import { history, request } from '@umijs/max';
import { Button, Collapse, Flex, List, Tag, Typography } from 'antd';
import dayjs from 'dayjs';
import React, { useEffect, useRef, useState } from 'react';
import { Report } from 'typings';
import { saveAs } from 'file-saver';
import { useLocalStorage } from '@uidotdev/usehooks';


export default () => {
  const [_, setReportID] = useLocalStorage<number>('reportID', 0);
  const [reports, setReports] = useState<Report[]>([]);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  
  const handleShowMore = (reportID: number) => {
    setReportID(reportID);
    history.push("/home");
  }

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
        return (
          <CollapsedTemps report={report}/>
        );
      },
    },
    {
      width: 200,
      render(_, report) {
        return (
          <Flex vertical gap={10}>
            <Button type='default' onClick={() => saveReportAsXML(report.id)}>Сохранить как XML</Button>
            <Button type='default' onClick={() => handleShowMore(report.id)}>Подробнее</Button>
          </Flex>
        ) 
        
      }
    }
  ];
  
  const saveReportAsXML = (reportID: number) => {
      request(`http://localhost:5114/api/HeatLoss/GetXmlReport?requestID=${reportID}`,
        {
          method: "POST",
          headers: {
            'Content-Type': 'application/xml'
          },
          responseType: 'blob'
        }
      )
      .then((file: Blob) => saveAs(file, `Heat loss report ${dayjs(new Date()).format('DD.MM.YY HH:mm:ss')}.xml`))
  }
  
  useEffect(() => {
    setIsLoading(true);
    request('http://localhost:5114/api/HeatLoss/GetReports', {
      method: 'POST',
      data: {},
    }).then((response: Report[]) => { setReports(response); setIsLoading(false); });
  }, []);

  return (
    <PageContainer
      title="Reports"
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
          padding: '1em',
        }}
      >
        <ProTable 
        loading={isLoading}
        search={false}
        dataSource={reports.map(r => ({
            ...r,
            ql: r.ql.toFixed(3),
            q: r.q.toFixed(3),
            a1: r.a1.toFixed(3),
            a2: r.a2.toFixed(3),
        }))} columns={reportsColsStructure}
        style={{
            overflowX: 'hidden'
        }}
        scroll={{
            y: '20em',
            x: '100%'
        }} />
      </ProCard>
    </PageContainer>
  );
};
