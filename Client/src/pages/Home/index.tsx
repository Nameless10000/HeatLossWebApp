import {
  PageContainer,
  ProFormDigit,
  ProFormSelect,
} from '@ant-design/pro-components';
import { request } from '@umijs/max';
import { Flex, Form } from 'antd';
import { CalculateRequestDTO, Material, Report } from 'typings';
import styles from './index.less';
import FormList from '@/components/FormList';

enum PipeOrientation {
  Vertical = 1,
  Horizontal = 2,
}

const HomePage: React.FC = () => {
  const onFormFinish = (data: CalculateRequestDTO) => {
    request('http://localhost:5114/api/HeatLoss/CalcHeatLossReport', {
      method: 'POST',
      data,
    }).then((response: Report) => console.log({ response }));
  };

  return (
    <PageContainer ghost title="Calculator">
      <Flex className={styles.container} dir="vertical" justify="center">
        <Form onFinish={onFormFinish} style={{ width: '80%' }}>
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
          <ProFormDigit
            name="precision"
            label="Precision, decimal signs"
            required
            min={0}
            rules={[{ required: true }]}
          />
          <FormList name="pipeLayers"/>
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
        </Form>
      </Flex>
    </PageContainer>
  );
};

export default HomePage;
