import { Collapse, Flex, List, Tag, Typography } from 'antd';
import { useState } from 'react';
import { Report } from 'typings';

export type CollapseTempsProps = {
  report: Report;
};

export default ({ report }: CollapseTempsProps) => {
  const [activeKey, setActiveKey] = useState<string | string[]>([]);

  const dataSource = report.temperatures.map((temp, i) => ({
    distance: report.radiuses[i].value,
    temperature: temp.value,
  }));

  return (
    <Collapse
      ghost
      onChange={(activeKeys) => setActiveKey(activeKeys)}
      items={[
        {
          key: '1',
          label: activeKey != '1' && !activeKey.includes('1') ? 'Смотреть' : 'Скрыть',
          children: (
            <List<{
              distance: number;
              temperature: number;
            }>
              dataSource={dataSource}
              renderItem={(item) => (
                <List.Item key={item.distance}>
                  <Flex justify="space-between" key={item.distance} style={{width: '100%'}}>
                    <Tag color="blue">{item.distance.toFixed(3)}m</Tag>
                    <Typography.Text>
                      {item.temperature.toFixed(3)}
                    </Typography.Text>
                  </Flex>
                </List.Item>
              )}
            />
          ),
        },
      ]}
    />
  );
};
