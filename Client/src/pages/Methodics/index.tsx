import exampleImage from '@/assets/image_2024-11-02_14-22-30.png';
import {
  BgColorsOutlined,
  CalculatorOutlined,
  EnvironmentOutlined,
  FireOutlined,
  HeatMapOutlined,
  LineOutlined,
  PartitionOutlined,
  RadiusSettingOutlined,
} from '@ant-design/icons';
import { PageContainer, ProCard } from '@ant-design/pro-components';
import { Flex, Image, List, Typography } from 'antd';

export default () => {
  const parameterList = [
    { text: 'Внутренний радиус, м', icon: <RadiusSettingOutlined /> },
    { text: 'Длина трубы, м', icon: <LineOutlined /> },
    {
      text: 'Коэффициент теплоотдачи от горячего флюида к стенке, Вт/(м²·К)',
      icon: <HeatMapOutlined />,
    },
    { text: 'Степень черноты поверхности трубы', icon: <BgColorsOutlined /> },
    { text: 'Температура внутри трубы, °C', icon: <FireOutlined /> },
    { text: 'Температура окружающей среды, °C', icon: <EnvironmentOutlined /> },
    { text: 'Точность вычислений', icon: <CalculatorOutlined /> },
    {
      text: 'Слои стенки',
      icon: <PartitionOutlined />,
      subItems: ['Толщина слоя, м', 'Материал слоя'],
    },
    { text: 'Расположение стенки' },
  ];

  return (
    <>
      <PageContainer title="Методические указания">
        <ProCard
          style={{
            borderRadius: '2em',
            boxShadow: '0 0 1em gray',
            padding: '1em',
          }}
        >
          <Flex align="center" style={{ width: '100%' }} justify="space-around">
            <Image src={exampleImage} style={{borderRadius: '1rem'}}>Heat loss throw multilayer pipe</Image>
            <Flex
              vertical
              align="start"
              justify="space-evenly"
              style={{ width: '70%', marginLeft: '1rem', marginRight: '1rem' }}
            >
              <Typography.Title level={3}>Входящие параметры</Typography.Title>
              <List
                style={{ width: '100%' }}
                dataSource={parameterList}
                renderItem={(item, index) => (
                  <List.Item style={{ display: 'flex', alignItems: 'left' }}>
                    <Flex
                      align="center"
                    >
                      <Typography.Text style={{ marginLeft: '0.5rem' }}>
                        {item.text}
                      </Typography.Text>
                      {item.subItems && (
                        <List
                        style={{marginLeft: '1rem'}}
                          dataSource={item.subItems}
                          renderItem={(subItem) => (
                            <List.Item style={{ paddingLeft: '0.5rem' }}>
                              <Typography.Text>- {subItem}</Typography.Text>
                            </List.Item>
                          )}
                        />
                      )}
                    </Flex>
                    <Typography.Text>{item.icon}</Typography.Text>
                  </List.Item>
                )}
              />
            </Flex>
          </Flex>
        </ProCard>
      </PageContainer>
    </>
  );
};
